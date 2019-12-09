namespace pmonidentity.Domains.Utilities {
	public interface IUtlPasswordHasher {
		string HashPassword(string input);
		bool ValidatePassword(string fromUserInput, string fromDb);
	}
}