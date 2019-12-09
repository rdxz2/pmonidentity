using Microsoft.AspNetCore.Identity;

namespace pmonidentity.Models {
	public class ApplicationUser : IdentityUser {
		public string name { get; set; }
		public string nik { get; set; }
		public string email { get; set; }
		public string ext { get; set; }
	}
}