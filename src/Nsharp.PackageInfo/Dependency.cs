using System;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Dependency {

		[Required]
		public Uri Url { get; set; }

		[Required]
		public DependencyType Type { get; set; }

	}

}
