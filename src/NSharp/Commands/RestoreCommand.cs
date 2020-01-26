using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
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
			var downloadResult = await cmakeSetup.DownloadCmakeAsync(cancellationToken);
			return 0;
		}

		private class CmakeSetup {

			private Uri linux64Url = new Uri("https://github.com/Kitware/CMake/releases/download/v3.16.3/cmake-3.16.3-Linux-x86_64.tar.gz");
			private Uri windows64Url = new Uri("https://github.com/Kitware/CMake/releases/download/v3.16.3/cmake-3.16.3-win64-x64.zip");

			public async Task<int> DownloadCmakeAsync(CancellationToken cancellationToken = default) {
				using var httpClient = new HttpClient();
				HttpResponseMessage httpResponseMessage = null;
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
					httpResponseMessage = await httpClient.GetAsync(windows64Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
				}
				else {
					throw new Exception("Unsupported Operating System");
				}
				if (!httpResponseMessage.IsSuccessStatusCode) { return -1; }
				using var inputStream = await httpResponseMessage.Content.ReadAsStreamAsync();


				FileInfo file = null;
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
					file = new FileInfo($"{AppContext.BaseDirectory}tmp/cmake.zip");
				}
				file.Directory.Create();
				using var outputFile = file.Create();
				await inputStream.CopyToAsync(outputFile, cancellationToken);

				return 0;
			}

		}

	}

}
