using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Downloaders {

	public class ZipDownloader : IDownloader {

		public async Task DownloadAsync(Uri uri, DirectoryInfo directoryInfo, CancellationToken cancellationToken = default) {
			using var httpClient = new HttpClient {
				DefaultRequestVersion = new Version(2, 0),
			};
			using var httpResponseMessage = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			using var httpContentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
			using var zipArchive = new ZipArchive(httpContentStream, ZipArchiveMode.Read);
			foreach (var zipArchiveEntry in zipArchive.Entries) {
				Console.WriteLine(zipArchiveEntry.FullName);
			}
		}

	}

}
