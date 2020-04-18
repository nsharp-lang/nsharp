using System.Threading.Tasks;
using Nsharp.Commands;
using Xunit;

namespace Nsharp.Tests.Commands {

	public class ToolchainUpdateLlvmCommandTest {

		private readonly ToolchainUpdateLlvmCommand toolchainUpdateLlvmCommand = new ToolchainUpdateLlvmCommand();

		[Fact]
		public async Task InvokeAsyncTest() {
			await this.toolchainUpdateLlvmCommand.InvokeAsync(default);
		}

	}

}
