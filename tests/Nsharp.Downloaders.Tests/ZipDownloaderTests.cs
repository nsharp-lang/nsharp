using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Nsharp.Downloaders.Tests {

	public class ZipDownloaderTests {

		private readonly DirectoryInfo baseDirectoryInfo = new DirectoryInfo($"{AppContext.BaseDirectory}ZipDownloaderTests/");

		[Fact]
		public async Task Fact1() {
			var zipDownloader = new ZipDownloader();
			var uri = new Uri("https://github.com/ninja-build/ninja/archive/v1.10.0.zip");
			var directoryInfo = baseDirectoryInfo.CreateSubdirectory("ninja-v1.10.0/");
			await zipDownloader.DownloadAsync(uri, directoryInfo);
		}

	}

}
