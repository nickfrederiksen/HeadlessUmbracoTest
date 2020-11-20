using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using HeadlessUmbracoTest.Core.Helpers;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.WebApi;

namespace HeadlessUmbracoTest.Core.ActionFilters
{
	public class UmbracoDomainFilterAttribute : UmbracoPageFilterAttribute
	{
		protected override IPublishedContent FindContent(HttpActionContext actionContext, UmbracoApiController controller)
		{
			var cultureCode = actionContext.Request.TryGetCultureCodeFromRequest();
			if (string.IsNullOrWhiteSpace(cultureCode))
			{
				return null;
			}

			var cultureInfo = CultureInfo.GetCultureInfo(cultureCode);

			var domain = controller.UmbracoContext.Domains.GetAll(false).FirstOrDefault(d => d.Culture == cultureInfo);

			if (domain == null)
			{
				return null;
			}

			var content = controller.UmbracoContext.Content.GetById(domain.ContentId);

			return content;
		}
	}
}
