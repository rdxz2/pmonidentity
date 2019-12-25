using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.Domains.Responses;
using pmonidentity.Domains.Services;
using pmonidentity.Domains.Utilities;
using pmonidentity.InputModels;

namespace pmonidentity.Services {
	public class SvsUser : SvsBase, ISvsUser {
		private readonly IRepoUser _repoUser;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUtlPasswordHasher _utlPasswordHasher;

		public SvsUser(
			IRepoUser repoUser,
			IUnitOfWork unitOfWork,
			IUtlPasswordHasher utlPasswordHasher
		) {
			_repoUser = repoUser;
			_unitOfWork = unitOfWork;
			_utlPasswordHasher = utlPasswordHasher;

		}

		public async Task<ResBase> Register(IMUser.Register input) {
			// generate shorthand for user's name
			IEnumerable<string> nameInitials = input.name.Split(' ').Select(m => m.Substring(0, 1));
			string nameShorthand = string.Join("", nameInitials);
			// limit to 2 characters only
			string nameShorthandLimited = nameShorthand.Substring(0, Math.Min(2, nameShorthand.Length));

			var tbiUser = new user {
				username = input.username,
				password = _utlPasswordHasher.HashPassword(input.password),
				user_detail = new user_detail {
					name = input.name,
					name_shorthand = nameShorthandLimited,
					nik = input.nik,
					email = input.email,
					ext = input.ext
				}
			};

			try {
				// insert
				await _repoUser.Insert(tbiUser);

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
			var tbuUser = await _repoUser.GetOne(username);
			if (tbuUser == null) return new ResBase($"user {username} not found");

			// validate user's old password
			if (!_utlPasswordHasher.ValidatePassword(input.oldPassword, tbuUser.password)) return new ResBase($"old password is incorrect");

			// edit header
			tbuUser.password = _utlPasswordHasher.HashPassword(input.newPassword);
			tbuUser.md_password = now;

			try {
				// update user
				_repoUser.Update(tbuUser);

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