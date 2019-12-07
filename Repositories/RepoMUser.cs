using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;

namespace pmonidentity.Repositories {
	public class RepoMUser : RepoBase, IRepoMUser {
		public RepoMUser(CtxPmondb ctxPmondb) : base(ctxPmondb) { }

		public async Task<m_user> GetOne(int id) {
			return await _ctxPmondb.m_user
				.Include(m => m.m_user_detail)
				.SingleOrDefaultAsync(m => m.id == id);
		}

		public async Task<m_user> GetOne(string username) {
			return await _ctxPmondb.m_user
				.Include(m => m.m_user_detail)
				.SingleOrDefaultAsync(m => m.username == username);
		}
	}
}