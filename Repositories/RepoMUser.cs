using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;

namespace pmonidentity.Repositories {
	public class RepoMUser : RepoBase, IRepoMUser {
		public RepoMUser(CtxPmonDb ctxPmonDb) : base(ctxPmonDb) { }

		public async Task<m_user> GetOne(int id) {
			return await _ctxPmonDb.m_user
				.Include(m => m.m_user_detail)
				.SingleOrDefaultAsync(m => m.id == id);
		}

		public async Task<m_user> GetOne(string username) {
			return await _ctxPmonDb.m_user
				.Include(m => m.m_user_detail)
				.SingleOrDefaultAsync(m => m.username == username);
		}

		public async Task Insert(m_user input) {
			await _ctxPmonDb.m_user.AddAsync(input);
		}
	}
}