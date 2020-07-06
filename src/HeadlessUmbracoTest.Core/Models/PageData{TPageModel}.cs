using System;

namespace HeadlessUmbracoTest.Core.Models
{
public class PageData<TPageModel> : ISimplePageData
{
	public string ContentType { get; set; }

	public Guid Id { get; set; }

	public string Name { get; set; }

	public DateTime CreateDate { get; set; }

	public DateTime UpdateDate { get; set; }

	public TPageModel Page { get; set; }
}
}
