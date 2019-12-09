using System;

namespace pmonidentity.Services {
	public abstract class SvsBase {
		protected readonly DateTime now;

		public SvsBase() {
			now = DateTime.Now;
		}
	}
}