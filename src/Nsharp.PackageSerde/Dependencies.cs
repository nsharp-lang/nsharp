using System.Collections.Generic;

namespace Nsharp.PackageSerde {

	public class Dependencies {

		public ICollection<Dependency>? Build { get; set; }

		public ICollection<Dependency>? Development { get; set; }

		public ICollection<Dependency>? Runtime { get; set; }

		public ICollection<object>? System { get; set; }

	}

}
