using pmonidentity.Domains.Models;

namespace pmonidentity.Repositories {
	public abstract class RepoBase {
		protected readonly CtxPmondb _ctxPmondb;

		public RepoBase(CtxPmondb ctxPmondb) {
			_ctxPmondb = ctxPmondb;
		}
	}
}