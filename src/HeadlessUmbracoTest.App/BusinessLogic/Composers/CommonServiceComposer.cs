// Copyright (c) LAIT ApS. All Rights Reserved.

using Umbraco.Core;
using Umbraco.Core.Composing;
using HeadlessUmbracoTest.App.BusinessLogic.Services;
using HeadlessUmbracoTest.App.Controllers.Utility;

namespace HeadlessUmbracoTest.App.BusinessLogic.Composers
{
	public class CommonServiceComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.RegisterAuto<IInjected>();
			composition.Register(typeof(RobotsTextController), Lifetime.Request);

		}
	}
}
