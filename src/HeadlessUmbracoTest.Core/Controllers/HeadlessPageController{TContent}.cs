using System.Web.Http;
using System.Web.Http.Results;
using HeadlessUmbracoTest.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace HeadlessUmbracoTest.Core.Controllers
{
	public abstract class HeadlessPageController<TContent> : HeadlessController, IHeadlessPageController
		where TContent : IPublishedContent
	{
		protected TContent CurrentPage => (TContent)this.GetCurrentPage();

		protected OkNegotiatedContentResult<PageData<TPageModel>> PageData<TPageModel>(TPageModel pageData)
		{
			PageData<TPageModel> model = this.WrapPageData(pageData);

			return this.Ok(model);
		}

		protected virtual PageData<TPageModel> WrapPageData<TPageModel>(TPageModel pageData)
		{
			var model = new PageData<TPageModel>()
			{
				Page = pageData,
			};

			this.MapSimplePageData(model);

			return model;
		}
	}
}
