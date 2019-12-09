using System.Threading.Tasks;
using pmonidentity.Domains.Models;

namespace pmonidentity.Domains.Repositories {
	public interface IRepoMUser {
		Task<m_user> GetOne(int id);
		Task<m_user> GetOne(string username);
		Task Insert(m_user input);
	}
}