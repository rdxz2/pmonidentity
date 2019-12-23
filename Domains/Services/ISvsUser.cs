using System.Threading.Tasks;
using pmonidentity.Domains.Responses;
using pmonidentity.InputModels;

namespace pmonidentity.Domains.Services {
	public interface ISvsUser {
		Task<ResBase> Register(IMUser.Register input);
		Task<ResBase> ChangePassword(string username, IMUser.ChangePassword input);
	}
}