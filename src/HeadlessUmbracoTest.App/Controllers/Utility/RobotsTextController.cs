// Copyright (c) LAIT ApS. All Rights Reserved.

using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace HeadlessUmbracoTest.App.Controllers.Utility
{
	public class RobotsTextController : Controller
	{
		private const string RobotsTxtDisallowAll = "RobotsTxtDisallowAll";
		private readonly string robotsTxtDisallow;

		public RobotsTextController()
		{
			this.robotsTxtDisallow = ConfigurationManager.AppSettings.Get(RobotsTxtDisallowAll);
		}

		[OutputCache(Duration = 86400)]
		public ContentResult RobotsText()
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("user-agent: *");
			if (bool.TryParse(this.robotsTxtDisallow, out bool setDisallow) && setDisallow)
			{
				stringBuilder.AppendLine("Disallow: /");
			}

			return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
		}
	}
}
