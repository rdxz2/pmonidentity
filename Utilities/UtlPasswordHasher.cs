using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using pmonidentity.Domains.Utilities;

namespace pmonidentity.Utilities {
	public class UtlPasswordHasher : IUtlPasswordHasher {
		// password hasher
		public string HashPassword(string input) {
			var salt = GenerateSalt(16);
			var bytes = Hash(input, salt);
			return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
		}

		// validate password
		public bool ValidatePassword(string fromUserInput, string fromDb) {
			try {
				var parts = fromDb.Split(':');
				var salt = Convert.FromBase64String(parts[0]);
				var bytes = Hash(fromUserInput, salt);
				return parts[1].Equals(Convert.ToBase64String(bytes));
			}
			catch {
				return false;
			}
		}

		// utils
		// salt generator
		private static byte[] GenerateSalt(int length) {
			var salt = new byte[length];
			using (var random = RandomNumberGenerator.Create()) {
				random.GetBytes(salt);
			}
			return salt;
		}
		// hash string with salt
		private byte[] Hash(string input, byte[] salt) {
			return KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
		}
	}
}