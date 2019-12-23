using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pmonidentity.Domains.Services;
using pmonidentity.Extensions;
using pmonidentity.InputModels;

namespace pmonidentity {
	[Route("/[controller]")]
	[Authorize]
	public class UserController : ControllerBase {
		private readonly ISvsUser _svsMUser;

		public UserController(
			ISvsUser svsEditUser
		) {
			_svsMUser = svsEditUser;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] IMUser.Register input) {
			if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
			var res = await _svsMUser.Register(input);
			if (!res._rs) return BadRequest(res._rt);
			return Ok(res._rt);
		}

		[HttpPost("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody] IMUser.ChangePassword input) {
			if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
			var res = await _svsMUser.ChangePassword(User.Identity.Name, input);
			if (!res._rs) return BadRequest(res._rt);
			return Ok(res._rt);
		}
	}
}