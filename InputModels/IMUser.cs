using System.ComponentModel.DataAnnotations;

namespace pmonidentity.InputModels {
	public class IMUser {
		public class Edit {
			[Required(ErrorMessage = "name is required")] public string name { get; set; }
			[Required(ErrorMessage = "worker identity number is required")] public string nik { get; set; }
			[Required(ErrorMessage = "email is required")] public string email { get; set; }
			[Required(ErrorMessage = "extension number is required")] public string ext { get; set; }
		}

		public class Register {
			[Required(ErrorMessage = "username is required")] public string username { get; set; }
			[Required(ErrorMessage = "password is required")] public string password { get; set; }
			[Required(ErrorMessage = "name is required")] public string name { get; set; }
			[Required(ErrorMessage = "worker identity number is required")] public string nik { get; set; }
			[Required(ErrorMessage = "email is required")] public string email { get; set; }
			[Required(ErrorMessage = "extension number is required")] public string ext { get; set; }
		}

		public class ChangePassword {
			[Required(ErrorMessage = "old password is required")] public string oldPassword { get; set; }
			[Required(ErrorMessage = "new password is required")] public string newPassword { get; set; }
		}
	}
}