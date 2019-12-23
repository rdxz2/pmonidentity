using System;
using System.Threading.Tasks;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.Domains.Responses;
using pmonidentity.Domains.Services;
using pmonidentity.Domains.Utilities;
using pmonidentity.InputModels;

namespace pmonidentity.Services {
	public class SvsUser : SvsBase, ISvsUser {
		private readonly IRepoMUser _repoMUser;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUtlPasswordHasher _utlPasswordHasher;

		public SvsUser(
			IRepoMUser repoMUser,
			IUnitOfWork unitOfWork,
			IUtlPasswordHasher utlPasswordHasher
		) {
			_repoMUser = repoMUser;
			_unitOfWork = unitOfWork;
			_utlPasswordHasher = utlPasswordHasher;

		}

		public async Task<ResBase> Register(IMUser.Register input) {
			var tbiMUser = new user {
				username = input.username,
				password = _utlPasswordHasher.HashPassword(input.password),
				user_detail = new user_detail {
					name = input.name,
					nik = input.nik,
					email = input.email,
					ext = input.ext
				}
			};

			try {
				// insert
				await _repoMUser.Insert(tbiMUser);

				// commit
				await _unitOfWork.Commit();

				return new ResBase();
			}
			catch (Exception ex) {
				return new ResBase($"Server errror: {ex.Message}");
			}
		}

		public async Task<ResBase> ChangePassword(string username, IMUser.ChangePassword input) {
			// get user
			var tbuMUser = await _repoMUser.GetOne(username);
			if (tbuMUser == null) return new ResBase($"user {username} not found");

			// validate user's old password
			if (!_utlPasswordHasher.ValidatePassword(input.oldPassword, tbuMUser.password)) return new ResBase($"old password is incorrect");

			// edit header
			tbuMUser.password = _utlPasswordHasher.HashPassword(input.newPassword);
			tbuMUser.md_password = now;

			try {
				// update user
				_repoMUser.Update(tbuMUser);

				// commit
				await _unitOfWork.Commit();

				return new ResBase();
			}
			catch (Exception ex) {
				return new ResBase($"Server errror: {ex.Message}");
			}
		}
	}
}