// Copyright (c) LAIT ApS. All Rights Reserved.

using HeadlessUmbracoTest.Core.Components;
using Umbraco.Core.Composing;

namespace HeadlessUmbracoTest.App.BusinessLogic.Composers
{
	[ComposeAfter(typeof(IUserComposer))]
	public class RegisterCustomRouteComposer : ComponentComposer<RegisterCustomRouteComponent>
	{
	}
}
