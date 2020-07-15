using System.Text.Json;

namespace Nsharp.PackageInfo {

	public class PackageSerializer {

		private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
		};

		public Package Deserialize(string json) {
			return JsonSerializer.Deserialize<Package>(json, jsonSerializerOptions);
		}

	}

}
