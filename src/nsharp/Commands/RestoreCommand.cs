using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Commands {

	public class RestoreCommand : Command, ICommandHandler {

		public RestoreCommand() : base("restore") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			return await SetupCmake();
		}

		private async Task<int> SetupCmake(CancellationToken cancellationToken = default) {
			var cmakeSetup = new CmakeSetup();
			var result = await cmakeSetup.SetupWindows64(cancellationToken);
			return 0;
		}

		private class CmakeSetup {

			private FileInfo fileInfo = null;
			private readonly Uri linux64Url = new Uri("https://github.com/Kitware/CMake/releases/download/v3.16.3/cmake-3.16.3-Linux-x86_64.tar.gz");
			private readonly Uri windows64Url = new Uri("https://github.com/Kitware/CMake/releases/download/v3.16.3/cmake-3.16.3-win64-x64.zip");

			public async Task<int> SetupWindows64(CancellationToken cancellationToken = default) {
				using var httpClient = new HttpClient();
				using var httpResponseMessage = await httpClient.GetAsync(windows64Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
				if (!httpResponseMessage.IsSuccessStatusCode) { return -1; }
				using var inputStream = await httpResponseMessage.Content.ReadAsStreamAsync();
				using var zipArchive = new ZipArchive(inputStream);
				var zipArchiveEntry = zipArchive.Entries.First(w => w.Name == "cmake.exe");
				using var cmakeInputStream = zipArchiveEntry.Open();
				var fileInfo = new FileInfo($"{AppContext.BaseDirectory}cmake/cmake.exe");
				fileInfo.Directory.Create();
				using var cmakeOutputStream = fileInfo.OpenWrite();
				await cmakeInputStream.CopyToAsync(cmakeOutputStream, cancellationToken);
				return 0;
			}

			public async Task<int> DownloadAsync(CancellationToken cancellationToken = default) {
				using var httpClient = new HttpClient();
				HttpResponseMessage httpResponseMessage = null;
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
					httpResponseMessage = await httpClient.GetAsync(windows64Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
				}else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
					httpResponseMessage = await httpClient.GetAsync(linux64Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
				}
				else {
					throw new Exception("Unsupported Operating System");
				}
				if (!httpResponseMessage.IsSuccessStatusCode) { return -1; }
				using var inputStream = await httpResponseMessage.Content.ReadAsStreamAsync();


				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
					this.fileInfo = new FileInfo($"{AppContext.BaseDirectory}tmp/cmake.zip");
				}else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
					this.fileInfo = new FileInfo($"{AppContext.BaseDirectory}tmp/cmake.tar.gz");
				}
				else {
					throw new Exception("Unsupported Operating System");
				}
				this.fileInfo.Directory.Create();
				using var outputFile = this.fileInfo.Create();
				await inputStream.CopyToAsync(outputFile, cancellationToken);

				return 0;
			}

			public async Task<int> ExtractAsync(CancellationToken cancellationToken = default) {
				using var zipArchive = new ZipArchive(this.fileInfo.Open(FileMode.Open), ZipArchiveMode.Read);
				zipArchive.ExtractToDirectory($"{AppContext.BaseDirectory}tmp/cmake-source/");
				return await Task.FromResult(0);
			}

		}

	}

}
