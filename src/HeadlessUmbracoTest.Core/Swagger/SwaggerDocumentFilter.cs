using System.Linq;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace HeadlessUmbracoTest.Core.Swagger
{
	public class SwaggerDocumentFilter : IDocumentFilter
	{
		public void Apply(
			SwaggerDocument swaggerDoc,
			SchemaRegistry schemaRegistry,
			IApiExplorer apiExplorer)
		{
			swaggerDoc.paths = swaggerDoc
				.paths
				.Where(x => x.Key.StartsWith("/api"))
				.ToDictionary(e => e.Key, e => e.Value);

			foreach (var item in swaggerDoc.paths)
			{
				item.Value.options = null;
			}
		}
	}
}
