using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.PackageInfo {

	public static class PackageSerializer {

		private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
			Converters = {
				new JsonStringEnumConverter(),
			},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true,
		};

		public static Package Deserialize(string json) {
			return JsonSerializer.Deserialize<Package>(json, jsonSerializerOptions);
		}

		public static async ValueTask<Package> DeserializeAsync(FileInfo fileInfo, CancellationToken cancellationToken = default) {
			await using var fileStream = fileInfo.OpenRead();
			return await JsonSerializer.DeserializeAsync<Package>(fileStream, jsonSerializerOptions, cancellationToken);
		}

		public static async ValueTask<Package> DeserializeAsync(Stream utf8Json, CancellationToken cancellationToken = default) {
			return await JsonSerializer.DeserializeAsync<Package>(utf8Json, jsonSerializerOptions, cancellationToken);
		}

	}

}
