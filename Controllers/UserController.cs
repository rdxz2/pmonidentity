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
		private readonly ISvsUser _svsUser;

		public UserController(
			ISvsUser svsEditUser
		) {
			_svsUser = svsEditUser;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] IMUser.Register input) {
			if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
			var res = await _svsUser.Register(input);
			if (!res._rs) return BadRequest(res._rt);
			return Ok(res._rt);
		}

		[HttpPost("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody] IMUser.ChangePassword input) {
			if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
			var res = await _svsUser.ChangePassword(User.Identity.Name, input);
			if (!res._rs) return BadRequest(res._rt);
			return Ok(res._rt);
		}
	}
}