using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.PackageInfo {

	public class PackageSerializer {

		private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
		};

		public Package Deserialize(string json) {
			return JsonSerializer.Deserialize<Package>(json, jsonSerializerOptions);
		}

		public async ValueTask<Package> DeserializeAsync(FileInfo fileInfo, CancellationToken cancellationToken = default) {
			await using var fileStream = fileInfo.OpenRead();
			return await JsonSerializer.DeserializeAsync<Package>(fileStream, jsonSerializerOptions, cancellationToken);
		}

		public async ValueTask<Package> DeserializeAsync(Stream utf8Json, CancellationToken cancellationToken = default) {
			return await JsonSerializer.DeserializeAsync<Package>(utf8Json, jsonSerializerOptions, cancellationToken);
		}

	}

}
