using System.IO;

namespace Nsharp.Cmake {

	public class CmakeBuildOptions {

		public DirectoryInfo BuildDirectory { get; set; }

		public string? Config { get; set; }

		public uint? Parallel { get; set; }

	}

}
