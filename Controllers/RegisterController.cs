using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pmonidentity.Domains.Services;
using pmonidentity.Extensions;
using pmonidentity.InputModels;

namespace pmonidentity.Controllers {
	[Route("/[controller]")]
	public class RegisterController : ControllerBase {
		private readonly ISvsRegister _svsRegister;

		public RegisterController(
			ISvsRegister svsRegister
			) {
			_svsRegister = svsRegister;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] IMRegister input) {
			if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
			var res = await _svsRegister.Register(input);
			if (!res._rs) return BadRequest(res._rt);
			return Ok(res._rt);
		}
	}
}