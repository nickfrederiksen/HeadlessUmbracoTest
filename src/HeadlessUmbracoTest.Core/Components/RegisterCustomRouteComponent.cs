using System;
using System.Web.Http;
using HeadlessUmbracoTest.Core.Constants;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace HeadlessUmbracoTest.Core.Components
{
	public class RegisterCustomRouteComponent : IComponent
	{
		public void Initialize()
		{
			HttpConfiguration configuration = GlobalConfiguration.Configuration;

			configuration.Routes.MapHttpRoute(
				name: "contentPathApi",
				routeTemplate: "api/content/{contentGuid}",
				defaults: new
				{
					action = "Get",
					controller = "ContentApi",
					area = AreaNames.HeadlessUmbraco,
				});

			// This route is only for swagger, that, for some reason cannot identify an optional parameter.
			configuration.Routes.MapHttpRoute(
				name: "sitemapRootApi",
				routeTemplate: "api/sitemap",
				defaults: new
				{
					action = "GetRoot",
					controller = "SitemapApi",
					area = AreaNames.HeadlessUmbraco,
				});

			configuration.Routes.MapHttpRoute(
				name: "sitemapApi",
				routeTemplate: "api/sitemap/{parentGuid}",
				defaults: new
				{
					action = "Get",
					controller = "SitemapApi",
					area = AreaNames.HeadlessUmbraco,
					parentGuid = RouteParameter.Optional
				});
		}

		public void Terminate()
		{
		}
	}
}
