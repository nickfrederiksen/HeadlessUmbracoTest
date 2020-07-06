using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using HeadlessUmbracoTest.Core.Controllers;
using Umbraco.Web.WebApi;

namespace HeadlessUmbracoTest.Core.Helpers
{
	public static class ControllerHelper
	{
		private static readonly Type PageControllerType = typeof(IHeadlessPageController);
		private static readonly Type UmbracoApiControllerType = typeof(UmbracoApiController);

		public static readonly Lazy<Dictionary<string, HttpControllerDescriptor>> ControllerMappings = new Lazy<Dictionary<string, HttpControllerDescriptor>>(
			() =>
			{
				IHttpControllerSelector httpControllerSelector = GlobalConfiguration.Configuration.Services.GetHttpControllerSelector();
				IDictionary<string, HttpControllerDescriptor> controllerMappings = httpControllerSelector.GetControllerMapping();

				return controllerMappings.Where(c => PageControllerType.IsAssignableFrom(c.Value.ControllerType) && UmbracoApiControllerType.IsAssignableFrom(c.Value.ControllerType)).ToDictionary(c => c.Key.ToLower(), c => c.Value);
			},
			true);
	}
}
