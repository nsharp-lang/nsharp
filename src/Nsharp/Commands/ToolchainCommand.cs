using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Nsharp.Commands {

	public class ToolchainCommand : Command, ICommandHandler {

		public ToolchainCommand() : base("toolchain") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			return await Task.FromResult(0);
		}

	}

}
