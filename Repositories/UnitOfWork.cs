using System.Threading.Tasks;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;

namespace pmonidentity.Repositories {
	public class UnitOfWork : RepoBase, IUnitOfWork {

		public UnitOfWork(CtxPmonDb ctxPmonDb) : base(ctxPmonDb) { }

		public async Task Commit() {
			await _ctxPmonDb.SaveChangesAsync();
		}
	}
}