using System.Threading.Tasks;

namespace pmonidentity.Domains.Repositories {
	public interface IUnitOfWork {
		Task Commit();
	}
}