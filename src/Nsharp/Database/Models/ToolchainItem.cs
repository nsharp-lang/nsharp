using System;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.Database.Models {

	public class ToolchainItem {

		public string Sha512Hash { get; set; }

		[Key]
		public string Package { get; set; }

		[Key]
		public string Target { get; set; }

		[Key]
		public Version Version { get; set; }

	}

}
