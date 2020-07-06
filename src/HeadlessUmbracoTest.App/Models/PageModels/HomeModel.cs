using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HeadlessUmbracoTest.App.Models.PageModels
{
	public class HomeModel
	{
		public JToken BodyText { get; internal set; }

		public string Header { get; internal set; }
	}
}
