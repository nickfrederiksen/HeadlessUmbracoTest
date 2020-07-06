using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HeadlessUmbracoTest.Core.Helpers;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Routing;
using Umbraco.Web.WebApi;

namespace HeadlessUmbracoTest.Core.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class UmbracoPageFilterAttribute : ActionFilterAttribute
	{
		public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{

			if (actionContext.ControllerContext.Controller is UmbracoApiController controller)
			{
				var content = HandleContent(actionContext, controller);
				if (content == null)
				{
					actionContext.Response = await new System.Web.Http.Results.NotFoundResult(controller).ExecuteAsync(cancellationToken);
				}

				await base.OnActionExecutingAsync(actionContext, cancellationToken);
			}
		}

		protected virtual IPublishedContent HandleContent(HttpActionContext actionContext, UmbracoApiController controller)
		{
			var cultureCode = actionContext.Request.TryGetCultureCodeFromRequest();
			controller.UmbracoContext.VariationContextAccessor.VariationContext = new VariationContext(cultureCode);

			IPublishedContent content = FindContent(actionContext, controller);

			// Validate content and culture:
			if (content == null || !content.Cultures.Any(c => c.Value.Culture.Equals(cultureCode, StringComparison.InvariantCultureIgnoreCase)))
			{
				return null;
			}

			var router = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IPublishedRouter)) as IPublishedRouter;

			var contentRequest = router.CreateRequest(controller.UmbracoContext, new Uri(content.Url, UriKind.RelativeOrAbsolute));

			contentRequest.PublishedContent = content;

			if (!router.PrepareRequest(contentRequest))
			{
				router.UpdateRequestToNotFound(contentRequest);
			}

			controller.UmbracoContext.PublishedRequest = contentRequest;
			controller.Umbraco.AssignedContentItem = content;

			return contentRequest.PublishedContent;
		}

		protected virtual IPublishedContent FindContent(HttpActionContext actionContext, UmbracoApiController controller)
		{
			string routeContentGuid = actionContext.Request.GetRouteData().Values["contentGuid"] as string;
			if (string.IsNullOrWhiteSpace(routeContentGuid) || !Guid.TryParse(routeContentGuid, out var contentGuid))
			{
				return null;
			}

			var content = controller.UmbracoContext.Content.GetById(contentGuid);

			return content;
		}
	}
}
