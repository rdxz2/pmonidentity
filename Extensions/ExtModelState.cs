using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace pmonidentity.Extensions {
	public static class ExtModelState {
		// get error messages for each error field
		public static string GetErrorMessages(this ModelStateDictionary dictionary) {
			return string.Join("; ", dictionary.SelectMany(m => m.Value.Errors)
				.Select(m => m.ErrorMessage)
				.ToList());
		}
	}
}