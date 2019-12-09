using System.ComponentModel.DataAnnotations;

namespace pmonidentity.InputModels {
	public class IMRegister {
		[Required(ErrorMessage = "username is required")] public string username { get; set; }
		[Required(ErrorMessage = "password is required")] public string password { get; set; }
		[Required(ErrorMessage = "name is required")] public string name { get; set; }
		[Required(ErrorMessage = "NIK is required")] public string nik { get; set; }
		[Required(ErrorMessage = "email is required")] public string email { get; set; }
		[Required(ErrorMessage = "extension number is required")] public string ext { get; set; }
	}
}