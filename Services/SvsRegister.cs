using System;
using System.Threading.Tasks;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.Domains.Responses;
using pmonidentity.Domains.Services;
using pmonidentity.Domains.Utilities;
using pmonidentity.InputModels;

namespace pmonidentity.Services {
	public class SvsRegister : SvsBase, ISvsRegister {
		private readonly IRepoMUser _repoMUser;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUtlPasswordHasher _utlPasswordHasher;

		public SvsRegister(
			IRepoMUser repoMUser,
			IUnitOfWork unitOfWork,
			IUtlPasswordHasher utlPasswordHasher
			) {
			_repoMUser = repoMUser;
			_unitOfWork = unitOfWork;
			_utlPasswordHasher = utlPasswordHasher;
		}

		public async Task<ResBase> Register(IMRegister input) {
			var tbiMUser = new m_user {
				username = input.username,
				password = _utlPasswordHasher.HashPassword(input.password),
				m_user_detail = new m_user_detail {
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
	}
}