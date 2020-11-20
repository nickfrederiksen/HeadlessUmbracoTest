// Copyright (c) LAIT ApS. All Rights Reserved.

using System.Web.Http;
using HeadlessUmbracoTest.App.Models.ModelsBuilder;
using HeadlessUmbracoTest.App.Models.PageModels;
using HeadlessUmbracoTest.Core.ActionFilters;
using HeadlessUmbracoTest.Core.Controllers;

namespace HeadlessUmbracoTest.App.Controllers
{
	[CacheControl(MaxAge = 600, IsPublic = false)]
	public class HomeController : HeadlessPageController<Home>
	{
		[HttpGet]
		public IHttpActionResult Index(int? test = null)
		{
			var model = new HomeModel()
			{
				Header = this.CurrentPage.HeroHeader,
			};

			return this.PageData(model);
		}
	}
}
