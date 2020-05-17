using System.IO;

namespace Nsharp.Cmake {

	public class CmakeConfigureOptions {

		public DirectoryInfo BuildDirectory { get; set; }

		public string? BuildType { get; set; }

		public DirectoryInfo SourceDirectory { get; set; }

	}

}
