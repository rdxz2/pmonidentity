using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.Domains.Utilities;
using Serilog;

namespace pmonidentity.IdentityServer {
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator {
		private readonly IRepoUser _repoUser;
		private readonly IUtlPasswordHasher _utlPasswordHasher;

		public ResourceOwnerPasswordValidator(
			IRepoUser repoUser,
			IUtlPasswordHasher utlPasswordHasher
			) {
			_repoUser = repoUser;
			_utlPasswordHasher = utlPasswordHasher;
		}

		// this is used to validate your user account with provided grant at /connect/token
		public async Task ValidateAsync(ResourceOwnerPasswordValidationContext input) {
			try {
				// get your user model from db (by username - in my case its email)
				var repoUser = await _repoUser.GetOne(input.UserName);
				if (repoUser != null) {
					// check if password match - remember to hash password if stored as hash in db
					if (_utlPasswordHasher.ValidatePassword(input.Password, repoUser.password)) {
						// set the result
						input.Result = new GrantValidationResult(
								subject: repoUser.username.ToString(),
								authenticationMethod: "custom",
								claims: GetUserClaims(repoUser.user_detail));

						return;
					}

					input.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
					return;
				}
				input.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
				return;
			}
			catch (Exception ex) {
				input.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
				Log.Error($"Err: {ex.Message}");
			}
		}

		// build claims array from user data
		public static Claim[] GetUserClaims(user_detail repoUserDetail) {
			return new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.UniqueName, repoUserDetail.id_userNavigation.username ?? ""),
				// new Claim(JwtClaimTypes.Name, repoUserDetail.name ?? ""),
				new Claim("nameShorthand", repoUserDetail.name_shorthand),
				// new Claim(JwtClaimTypes.Email, repoUserDetail.email  ?? ""),
				// new Claim(JwtClaimTypes.PhoneNumber, "0815171628347"  ?? ""),
				// new Claim("ext", repoUserDetail.ext ?? ""),
				// new Claim("warehouse", "WAREHOUSE"),
				// new Claim("branch", "BRANCH"),
				// new Claim("unit", "UNIT")
			};
		}
	}
}