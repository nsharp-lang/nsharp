using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nsharp.Database;

namespace Nsharp.Commands {

	public class ToolchainUpdateCmakeCommand : Command, ICommandHandler {

		private readonly NsharpDbContext nsharpDbContext = new NsharpDbContext();
		private readonly Version Version = new Version(3, 17, 1);
		private readonly DirectoryInfo buildDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/cmake/build/");
		private readonly DirectoryInfo sourceDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/cmake/source/");

		public ToolchainUpdateCmakeCommand() : base("cmake") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			var cancellationToken = context.GetCancellationToken();

			var downloadResult = await this.DownloadAsync(cancellationToken);
			if (downloadResult != 0) { return downloadResult; }

			var configureResult = this.Configure();
			if (configureResult != 0) { return configureResult; }

			return 0;
		}

		private async Task<int> DownloadAsync(CancellationToken cancellationToken = default) {
			using var httpClient = new HttpClient();
			using var httpResponseMessage = await httpClient.GetAsync($"https://github.com/Kitware/CMake/archive/v{this.Version.Major}.{this.Version.Minor}.{this.Version.Build}.zip", HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			if (!httpResponseMessage.IsSuccessStatusCode) { return -1; }

			var filename = httpResponseMessage.Content.Headers.ContentDisposition.FileName;
			await using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();

			using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
			sourceDirectoryInfo.Create();
			var firstEntryFullName = zipArchive.Entries.FirstOrDefault().FullName;
			foreach(var entry in zipArchive.Entries.Skip(1)) {
				var correctName = entry.FullName.Remove(0, firstEntryFullName.Length);
				var fileOrDirectoryPath =this.sourceDirectoryInfo.FullName + correctName;
				if (fileOrDirectoryPath.EndsWith('/')) {
					Directory.CreateDirectory(fileOrDirectoryPath);
				}
				else {
					entry.ExtractToFile(fileOrDirectoryPath, true);
				}
			}

			return 0;
		}

		private int Configure() {
			this.buildDirectoryInfo.Create();
			var processStartInfo = new ProcessStartInfo {
				Arguments = $"-DCMAKE_BUILD_TYPE=Release -S {this.sourceDirectoryInfo.FullName} -B {this.buildDirectoryInfo.FullName}",
				FileName = "cmake"
			};
			var process = System.Diagnostics.Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode;
		}

	}

}

