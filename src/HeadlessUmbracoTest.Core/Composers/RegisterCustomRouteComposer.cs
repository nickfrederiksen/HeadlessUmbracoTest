using HeadlessUmbracoTest.Core.Components;
using Umbraco.Core.Composing;

namespace HeadlessUmbracoTest.Core.Composers
{
	[RuntimeLevel(MinLevel = Umbraco.Core.RuntimeLevel.Run)]
	public class RegisterCustomRouteComposer : ComponentComposer<RegisterCustomRouteComponent>
	{
	}
}
