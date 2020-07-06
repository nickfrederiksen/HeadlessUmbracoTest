using System.Web.Http;
using System.Web.Http.Cors;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace HeadlessUmbracoTest.Core.Composers
{
	[RuntimeLevel(MinLevel = RuntimeLevel.Boot)]
	public class CorsComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			// Find a better way to do CORS:
			var cors = new EnableCorsAttribute("*", "*", "*");

			GlobalConfiguration.Configuration.EnableCors(cors);
		}
	}
}
