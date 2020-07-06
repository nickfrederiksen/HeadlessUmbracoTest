using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadlessUmbracoTest.Core.Models
{
	public class SitemapContentModel : ISimplePageData
	{
		public string ContentType { get; set; }

		public DateTime CreateDate { get; set; }

		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime UpdateDate { get; set; }

		public bool HasChildren { get; set; }

		public Guid? ParentId { get; set; }

		public int Level { get; set; }
	}
}
