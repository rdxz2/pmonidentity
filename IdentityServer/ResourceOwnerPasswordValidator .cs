using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using Serilog;

namespace pmonidentity.IdentityServer {
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator {
		//repository to get user from db
		private readonly IRepoMUser _repoMUser;

		public ResourceOwnerPasswordValidator(IRepoMUser repoMUser) {
			_repoMUser = repoMUser; //DI
		}

		//this is used to validate your user account with provided grant at /connect/token
		public async Task ValidateAsync(ResourceOwnerPasswordValidationContext input) {
			try {
				//get your user model from db (by username - in my case its email)
				var repoMuser = await _repoMUser.GetOne(input.UserName);
				if (repoMuser != null) {
					//check if password match - remember to hash password if stored as hash in db
					if (repoMuser.password == input.Password) {
						//set the result
						input.Result = new GrantValidationResult(
								subject: repoMuser.username.ToString(),
								authenticationMethod: "custom",
								claims: GetUserClaims(repoMuser.m_user_detail));

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

		//build claims array from user data
		public static Claim[] GetUserClaims(m_user_detail repoMUserDetail) {
			return new Claim[]
			{
				new Claim(JwtClaimTypes.Name, repoMUserDetail.name ?? ""),
				new Claim(JwtClaimTypes.Email, repoMUserDetail.email  ?? ""),
				new Claim(JwtClaimTypes.PhoneNumber, "0815171628347"  ?? ""),
				new Claim("ext", repoMUserDetail.ext ?? ""),
				new Claim("warehouse", "WAREHOUSE"),
				new Claim("branch", "BRANCH"),
				new Claim("unit", "UNIT")
			};
		}
	}
}