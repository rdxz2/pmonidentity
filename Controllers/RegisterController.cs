using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pmonidentity.Domains.Services;
using pmonidentity.Extensions;
using pmonidentity.InputModels;

namespace pmonidentity.Controllers {
	[Route("/[controller]")]
	[AllowAnonymous]
	public class RegisterController : ControllerBase {
		private readonly ISvsRegister _svsRegister;

		public RegisterController(
			ISvsRegister svsRegister
			) {
			_svsRegister = svsRegister;
		}
	}
}