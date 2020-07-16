using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.PackageInfo {

	public static class PackageSerializer {

		private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
			Converters = {
				new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase),
			},
			IgnoreNullValues = true,
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

		public static string Serialize(Package package) {
			return JsonSerializer.Serialize(package, jsonSerializerOptions);
		}

		public static async Task SerializeAsync(Package package, FileInfo fileInfo, CancellationToken cancellationToken = default) {
			await using var fileStream = fileInfo.OpenWrite();
			await JsonSerializer.SerializeAsync(fileStream, package, jsonSerializerOptions, cancellationToken);
		}

		public static async Task SerializeAsync(Package package, Stream utf8Json, CancellationToken cancellationToken = default) {
			await JsonSerializer.SerializeAsync(utf8Json, package, jsonSerializerOptions, cancellationToken);
		}

	}

}
