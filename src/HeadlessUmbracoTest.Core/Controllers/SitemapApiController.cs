using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using HeadlessUmbracoTest.Core.ActionFilters;
using HeadlessUmbracoTest.Core.Constants;
using HeadlessUmbracoTest.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace HeadlessUmbracoTest.Core.Controllers
{
	[PluginController(AreaNames.HeadlessUmbraco)]
	[UmbracoDomainFilter]
	public class SitemapApiController : HeadlessController
	{
		[HttpGet]
		[HttpOptions]
		public IHttpActionResult GetRoot()
		{
			return this.Get(null);
		}

		[HttpGet]
		[HttpOptions]
		public IHttpActionResult Get(Guid? parentGuid = null)
		{
			IEnumerable<IPublishedContent> children;
			if (parentGuid.HasValue)
			{
				var currentPage = this.Umbraco.Content(parentGuid);
				if (currentPage == null)
				{
					return this.NotFound();
				}

				children = currentPage.Children;
			}
			else
			{
				// If no parent id is present, return root content:
				children = this.Umbraco.ContentAtRoot();
			}

			var models = new List<SitemapContentModel>(children.Count());
			foreach (var page in children)
			{
				var model = new SitemapContentModel()
				{
					HasChildren = page.Children.Any(),
					Level = page.Level,
					ParentId = page.Parent?.Key,
				};
				this.MapSimplePageData(model, page);

				models.Add(model);
			}

			return this.Ok(models);
		}
	}
}
