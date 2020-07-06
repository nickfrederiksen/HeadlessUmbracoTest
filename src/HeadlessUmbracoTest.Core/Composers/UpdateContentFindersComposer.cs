using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace HeadlessUmbracoTest.Core.Composers
{
	public class UpdateContentFindersComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			// remove the core ContentFinderByUrl finder:
			// We don't need it when we are headless.
			composition.ContentFinders().Remove<ContentFinderByUrl>();
		}
	}
}
