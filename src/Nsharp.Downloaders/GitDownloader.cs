using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Downloaders {

	public class GitDownloader : IDownloader {

		public async Task DownloadAsync(Uri uri, DirectoryInfo directoryInfo, CancellationToken cancellationToken = default) {
			throw new NotImplementedException();
		}

	}

}
