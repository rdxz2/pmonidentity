namespace pmonidentity.Domains.Responses {

	public class ResBase {

		public bool _rs { get; set; }
		public string _rt { get; set; }

		// konstruktor (full)
		public ResBase(bool rs, string rt) {
			_rs = rs;
			_rt = rt;
		}

		// sukses
		public ResBase() : this(true, null) {

		}

		// gagal
		public ResBase(string rt) : this(false, rt) {

		}
	}
}