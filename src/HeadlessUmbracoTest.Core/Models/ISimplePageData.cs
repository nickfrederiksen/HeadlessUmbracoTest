using System;

namespace HeadlessUmbracoTest.Core.Models
{
	public interface ISimplePageData
	{
		string ContentType { get; set; }
		Guid Id { get; set; }
		string Name { get; set; }
		DateTime CreateDate { get; set; }
		DateTime UpdateDate { get; set; }
	}
}