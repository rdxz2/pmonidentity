using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;

namespace pmonidentity.Repositories {
	public class RepoMUser : RepoBase, IRepoMUser {
		public RepoMUser(CtxPmonDb ctxPmonDb) : base(ctxPmonDb) { }

		public async Task<user> GetOne(int id) {
			return await _ctxPmonDb.user
				.Include(m => m.user_detail)
				.SingleOrDefaultAsync(m => m.id == id);
		}

		public async Task<user> GetOne(string username) {
			return await _ctxPmonDb.user
				.Include(m => m.user_detail)
				.SingleOrDefaultAsync(m => m.username == username);
		}

		public async Task Insert(user input) {
			input.is_active = true;
			await _ctxPmonDb.user.AddAsync(input);
		}

		public void Update(user input) {
			_ctxPmonDb.user.Update(input);
		}
	}
}