using HeadlessUmbracoTest.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.WebApi;

namespace HeadlessUmbracoTest.Core.Controllers
{
	public abstract class HeadlessController : UmbracoApiController
	{
		protected virtual void MapSimplePageData(ISimplePageData model, IPublishedContent page = null)
		{
			if (page == null)
			{
				page = this.GetCurrentPage();
			}
			model.ContentType = page.ContentType.Alias;
			model.CreateDate = page.CreateDate;
			model.UpdateDate = page.UpdateDate;
			model.Name = page.Name;
			model.Id = page.Key;
		}

		protected IPublishedContent GetCurrentPage()
		{
			return this.Umbraco.AssignedContentItem;
		}
	}
}
