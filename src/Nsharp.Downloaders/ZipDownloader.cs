using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
			var zipArchiveEntries = zipArchive.Entries.Skip(1);
			foreach (var zipArchiveEntry in zipArchiveEntries) {
				if (!zipArchiveEntry.FullName.EndsWith('/')) {
					var trimmedFullName = zipArchiveEntry.FullName.Substring(zipArchiveEntry.FullName.IndexOf('/') + 1);
					var fileInfo = new FileInfo(directoryInfo.FullName + trimmedFullName);
					fileInfo.Directory.Create();
					zipArchiveEntry.ExtractToFile(fileInfo.FullName, true);
				}
			}
		}

	}

}
