using Xunit;

namespace Nsharp.PackageInfo.Tests {

	public class PackageSerializerTests {

		[Fact]
		public void Fact1() {
			var package = PackageSerializer.Deserialize(@"
				{
					""name"": ""fact-1"",
					""type"": ""library"",
					""version"": ""1.0.0-alpha3""
				}
			");
		}

	}

}
