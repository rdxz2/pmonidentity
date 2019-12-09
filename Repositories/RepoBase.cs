using pmonidentity.Domains.Models;

namespace pmonidentity.Repositories {
	public abstract class RepoBase {
		protected readonly CtxPmonDb _ctxPmonDb;

		public RepoBase(CtxPmonDb ctxPmonDb) {
			_ctxPmonDb = ctxPmonDb;
		}
	}
}