using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Nsharp.Cmake {

	public static class CmakeCommands {

		public static FileInfo CmakePath { get; } = GetCmakePath();

		public static void Build(CmakeBuildOptions options) {
			var processStartInfo = new ProcessStartInfo {
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
		}

		public static void Configure(CmakeConfigureOptions options) {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"-B", options.BuildDirectory.FullName,
					"-S", options.SourceDirectory.FullName,
					options.BuildType != null ? $"-DCMAKE_BUILD_TYPE={options.BuildType}" : "",
				},
				FileName = CmakePath.FullName,
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
		}

		public static void Install(CmakeInstallOptions options) {
			var processStartInfo = new ProcessStartInfo { };
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
		}

		private static FileInfo GetCmakePath() {
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && RuntimeInformation.ProcessArchitecture == Architecture.X64) {
				return new FileInfo($"{AppContext.BaseDirectory}tools/linux-x64/bin/cmake");
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && RuntimeInformation.ProcessArchitecture == Architecture.X64) {
				return new FileInfo($"{AppContext.BaseDirectory}tools/win-x64/bin/cmake.exe");
			}
			throw new NotSupportedException();
		}

	}

}
