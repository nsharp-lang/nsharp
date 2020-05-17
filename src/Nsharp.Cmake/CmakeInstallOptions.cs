using System.IO;

namespace Nsharp.Cmake {

	public class CmakeInstallOptions {

		public DirectoryInfo BuildDirectory { get; set; }

		public DirectoryInfo? Prefix { get; set; }

	}

}
