// Copyright (c) LAIT ApS. All Rights Reserved.

using System.Globalization;
using System.Web.Mvc;
using HeadlessUmbracoTest.App.Models.Shared;
using Umbraco.Web.Mvc;

namespace HeadlessUmbracoTest.App.BusinessLogic.ViewPages
{
	public abstract class AbstractViewPage : AbstractViewPage<dynamic>
	{
	}

#pragma warning disable SA1402 // FileMayOnlyContainASingleType
	public abstract class AbstractViewPage<TModel> : UmbracoViewPage<TModel>
	{
		internal const string GlobalModelViewDataKey = "global";

		public GlobalModel Global { get; private set; }

		public string CurrentLanguageCode => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

		protected override void SetViewData(ViewDataDictionary viewData)
		{
			if (viewData.TryGetValue(GlobalModelViewDataKey, out var globalObj) && globalObj is GlobalModel globalModel)
			{
				this.Global = globalModel;
			}

			base.SetViewData(viewData);
		}
	}
#pragma warning restore SA1402 // FileMayOnlyContainASingleType
}
