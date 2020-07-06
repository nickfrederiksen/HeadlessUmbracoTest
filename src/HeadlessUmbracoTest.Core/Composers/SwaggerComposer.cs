using System.Linq;
using System.Web.Http;
using HeadlessUmbracoTest.Core.Swagger;
using Swashbuckle.Application;
using Umbraco.Core.Composing;

namespace HeadlessUmbracoTest.Core.Composers
{
	public class SwaggerComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			GlobalConfiguration
				   .Configuration
				   .EnableSwagger(c =>
				   {
					   c.SingleApiVersion("v1", "HeadlessUmbracoTest");
					   c.ResolveConflictingActions(a => a.First());
					   c.OperationFilter<CultureCodeOperationFilter>();
					   c.DocumentFilter<SwaggerDocumentFilter>();
				   })
				   .EnableSwaggerUi();
		}
	}
}
