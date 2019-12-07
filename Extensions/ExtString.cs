namespace pmonidentity.Extensions {
	public static class ExtString {
		public static bool HasValue(this string input) {
			return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
		}
	}
}