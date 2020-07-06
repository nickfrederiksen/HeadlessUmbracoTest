using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Umbraco.Web.Routing;

namespace HeadlessUmbracoTest.Core.Helpers
{
	internal static class CultureCodeHelper
	{
		internal static string TryGetCultureCodeFromRequest(this HttpRequestMessage request)
		{
			var cultureCode = request.GetQueryNameValuePairs().FirstOrDefault(q => q.Key.Equals("culture", StringComparison.InvariantCultureIgnoreCase)).Value;
			if (string.IsNullOrWhiteSpace(cultureCode))
			{
				cultureCode = request.Headers.AcceptLanguage.FirstOrDefault()?.Value;
			}

			return cultureCode;
		}

		internal static string TryGetCultureCodeFromRequest(this PublishedRequest request)
		{
			var cultureCode = request.Uri.ParseQueryString().GetValues("culture")?.FirstOrDefault();

			if (string.IsNullOrWhiteSpace(cultureCode))
			{
				request.Headers.TryGetValue("Accept-Language", out cultureCode);
			}

			return cultureCode;
		}
	}
}
