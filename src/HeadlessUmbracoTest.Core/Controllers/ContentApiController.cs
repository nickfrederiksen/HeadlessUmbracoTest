// Copyright (c) LAIT ApS. All Rights Reserved.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using HeadlessUmbracoTest.Core.ActionFilters;
using HeadlessUmbracoTest.Core.Constants;
using HeadlessUmbracoTest.Core.Helpers;
using Umbraco.Core.Composing;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace HeadlessUmbracoTest.Core.Controllers
{
	[PluginController(AreaNames.HeadlessUmbraco)]
	[UmbracoPageFilter]
	[CacheControl(MaxAge = 900)]
	public class ContentApiController : UmbracoApiController
	{
		[HttpGet]
		public async Task<IHttpActionResult> Get(CancellationToken cancellationToken)
		{
			var controllerDescriptor = this.GetControllerDescriptor();
			if (controllerDescriptor != null)
			{
				try
				{
					var controller = controllerDescriptor.CreateController(this.Request);
					var controllerContext = new HttpControllerContext(this.RequestContext, this.Request, controllerDescriptor, controller);
					this.SanitizeRouteValues(controllerContext);

					var responseMessage = await controller.ExecuteAsync(controllerContext, cancellationToken);

					var result = this.ResponseMessage(responseMessage);

					return result;
				}
				catch (Exception ex)
				{
					Current.Logger.Error(typeof(ContentApiController), ex, $"Error requesting url '{this.Request.RequestUri}");

#if DEBUG
					return this.InternalServerError(ex);
#else
					return this.InternalServerError();
#endif
				}
			}

			return this.NotFound();
		}

		[HttpOptions]
		public IHttpActionResult Get()
		{
			if (this.GetControllerDescriptor() != null)
			{
				return this.Ok();
			}

			return this.NotFound();
		}

		private HttpControllerDescriptor GetControllerDescriptor()
		{
			var currentPage = this.UmbracoContext.PublishedRequest != null ? this.Umbraco.AssignedContentItem : null; ;
			if (currentPage != null && ControllerHelper.ControllerMappings.Value.TryGetValue(currentPage.ContentType.Alias.ToLower(), out var controllerDescriptor))
			{
				this.ControllerContext.RouteData.Values["action"] = "Index";
				this.ControllerContext.RouteData.Values["controller"] = this.Umbraco.AssignedContentItem.ContentType.Alias;

				return controllerDescriptor;
			}
			else
			{
				return null;
			}
		}

		private void SanitizeRouteValues(HttpControllerContext controllerContext)
		{
			var routeValue = controllerContext.RouteData.Values;
			if (routeValue.ContainsKey("area"))
			{
				routeValue.Remove("area");
			}
		}
	}
}
