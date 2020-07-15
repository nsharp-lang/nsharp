using System;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Package {

		[Required]
		public string Name { get; set; }

		[Required]
		public Type Type { get; set; }

		[Required]
		public Version Version { get; set; }

	}

}
