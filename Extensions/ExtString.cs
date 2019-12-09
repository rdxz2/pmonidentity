namespace pmonidentity.Extensions {
	public static class ExtString {
		// check if string has a valid value
		public static bool HasValue(this string input) {
			return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
		}
	}
}