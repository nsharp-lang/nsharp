using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Downloader.Core {

	public interface IDownloader {

		Task DownloadAsync(Uri uri, DirectoryInfo directoryInfo, CancellationToken cancellationToken = default);

	}

}
