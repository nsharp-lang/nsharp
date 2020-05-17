using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Nsharp.Ninja {

	public static class NinjaCommands {

		public static FileInfo NinjaPath { get; } = GetNinjaPath();

		private static FileInfo GetNinjaPath() {
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && RuntimeInformation.ProcessArchitecture == Architecture.X64) {
				return new FileInfo($"{AppContext.BaseDirectory}tools/linux-x64/ninja");
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && RuntimeInformation.ProcessArchitecture == Architecture.X64) {
				return new FileInfo($"{AppContext.BaseDirectory}tools/win-x64/ninja.exe");
			}
			throw new NotSupportedException();
		}

	}

}
