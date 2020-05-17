using System.IO;

namespace Nsharp.Cmake {

	public class CmakeConfigureOptions {

		public DirectoryInfo BuildDirectory { get; set; }

		public string? BuildType { get; set; }

		public string? Generator { get; set; }

		public FileInfo? MakeProgram { get; set; }

		public DirectoryInfo SourceDirectory { get; set; }

	}

}
