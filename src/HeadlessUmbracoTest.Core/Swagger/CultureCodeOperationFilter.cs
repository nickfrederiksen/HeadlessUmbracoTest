using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Swashbuckle.Swagger;

namespace HeadlessUmbracoTest.Core.Swagger
{
    public class CultureCodeOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline().Where(f => f.Scope == FilterScope.Action);

            if (operation.parameters == null)
			{
				operation.parameters = new List<Parameter>();
			}

			operation.parameters.Add(new Parameter
            {
                name = "Accept-Language",
                @in = "header",
                type = "string",
                @default = "en-US",
                description = "Set language code.",
                required = true
            });
        }
    }
}
