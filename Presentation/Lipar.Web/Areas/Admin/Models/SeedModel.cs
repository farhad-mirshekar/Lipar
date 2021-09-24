using System.Collections;

namespace Lipar.Web.Areas.Admin.Models
{
    public class SeedModel
    {
		public string schema { get; set; }
		public string typeName { get; set; }
		public int count { get; set; }
		public string name { get; set; }
		public int sendIndex { get; set; }

		public IList Items { get; set; }
	}
}
