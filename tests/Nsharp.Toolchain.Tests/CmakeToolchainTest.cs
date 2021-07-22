using System.Threading.Tasks;
using Xunit;

namespace Nsharp.Toolchain.Tests
{

	public class CmakeToolchainTest
    {

		private readonly CmakeToolchain cmakeToolchain = new CmakeToolchain();

        [Fact]
        public async Task DownloadAsyncFact()
        {
			await this.cmakeToolchain.DownloadAsync();
        }

    }

}
