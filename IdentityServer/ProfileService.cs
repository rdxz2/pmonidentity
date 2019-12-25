using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using pmonidentity.Domains.Repositories;
using pmonidentity.Extensions;
using Serilog;

namespace pmonidentity.IdentityServer {
	public class ProfileService : IProfileService {
		// services
		private readonly IRepoUser _repoUser;

		public ProfileService(IRepoUser repoUser) {
			_repoUser = repoUser;
		}

		// Get user profile date in terms of claims when calling /connect/userinfo
		public async Task GetProfileDataAsync(ProfileDataRequestContext context) {
			try {
				// depending on the scope accessing the user data.
				if (!string.IsNullOrEmpty(context.Subject.Identity.Name)) {
					// get user from db (in my case this is by email)
					var repoUser = await _repoUser.GetOne(context.Subject.Identity.Name);

					if (repoUser != null) {
						var claims = ResourceOwnerPasswordValidator.GetUserClaims(repoUser.user_detail);

						// set issued claims to return
						context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
					}
				}
				else {
					// get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
					// where and subject was set to my user id.
					var username = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

					if (username.Value.HasValue()) {
						// get user from db (find user by user id)
						var repoUser = await _repoUser.GetOne(username.Value);

						// issue the claims for the user
						if (repoUser != null) {
							var claims = ResourceOwnerPasswordValidator.GetUserClaims(repoUser.user_detail);

							context.IssuedClaims = claims.ToList();
							// context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
						}
					}
				}
			}
			catch (Exception ex) {
				Log.Error($"Err: {ex.Message}");
			}
		}

		// check if user account is active.
		public async Task IsActiveAsync(IsActiveContext context) {
			try {
				// get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
				var username = context.Subject.Claims.FirstOrDefault(m => m.Type == "sub");

				if (username.Value.HasValue()) {
					var user = await _repoUser.GetOne(username.Value);

					if (user != null) {
						context.IsActive = user.is_active;
					}
				}
			}
			catch (Exception ex) {
				Log.Error($"Err: {ex.Message}");
			}
		}
	}
}