using System.Threading.Tasks;
using pmonidentity.Domains.Responses;
using pmonidentity.InputModels;

namespace pmonidentity.Domains.Services {
	public interface ISvsMUser {
		Task<ResBase> Register(IMMUser.Register input);
		Task<ResBase> ChangePassword(string username, IMMUser.ChangePassword input);
	}
}