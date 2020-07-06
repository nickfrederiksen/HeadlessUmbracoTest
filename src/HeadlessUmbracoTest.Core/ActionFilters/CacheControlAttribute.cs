using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace HeadlessUmbracoTest.Core.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class CacheControlAttribute : ActionFilterAttribute
	{
		public int MaxAge { get; set; } = 3600;

		public bool IsPublic { get; set; } = true;

		public bool NoCache { get; set; } = false;

		public bool NoStore { get; set; } = false;

		public override void OnActionExecuted(HttpActionExecutedContext context)
		{
			if (context.Response != null && context.Response.Headers.CacheControl == null)
			{
				context.Response.Headers.CacheControl = new CacheControlHeaderValue()
				{
					Public = this.IsPublic,
					Private = !this.IsPublic,
					NoCache = this.NoCache,
					NoStore = this.NoStore,
					MaxAge = TimeSpan.FromSeconds(this.MaxAge),
				};
			}

			base.OnActionExecuted(context);
		}
	}
}
