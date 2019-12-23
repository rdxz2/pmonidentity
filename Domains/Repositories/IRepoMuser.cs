using System.Threading.Tasks;
using pmonidentity.Domains.Models;

namespace pmonidentity.Domains.Repositories {
	public interface IRepoMUser {
		Task<user> GetOne(int id);
		Task<user> GetOne(string username);
		Task Insert(user input);
		void Update(user input);
	}
}