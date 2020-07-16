using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nsharp.PackageInfo.Tests {

	public class PackageSerializerTests {

		[Fact]
		public void DeserializeFact1() {
			var jsonString = @"
				{
					""name"": ""nsharp-1-json"",
					""type"": ""library"",
					""version"": ""1.0.0-alpha3""
				}
			";
			var package = PackageSerializer.Deserialize(jsonString);
			Assert.Empty(package.Validate());
		}

		[Fact]
		public async Task DeserializeFact2() {
			var jsonString = @"
				{
					""name"": ""nsharp-1-json"",
					""type"": ""library"",
					""version"": ""1.0.0-alpha3""
				}
			";
			await using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
			var package = await PackageSerializer.DeserializeAsync(memoryStream);
			Assert.Empty(package.Validate());
		}

		[Fact]
		public async Task DeserializeFact3() {
			var fileInfo = new FileInfo($"{AppContext.BaseDirectory}1.nsharp.json");
			var package = await PackageSerializer.DeserializeAsync(fileInfo);
			Assert.Empty(package.Validate());
		}

		[Fact]
		public void SerializeFact1() {
			var package = new Package {
				Name = "nsharp-1-json",
				Type = Type.Library,
				Version = "1.0.0-alpha3"
			};
			var jsonString = PackageSerializer.Serialize(package);
			Assert.Empty(package.Validate());
		}

	}

}
