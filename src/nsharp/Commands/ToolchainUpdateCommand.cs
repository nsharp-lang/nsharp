using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Nsharp.Commands {

	public class ToolchainUpdateCommand : Command, ICommandHandler {

		public ToolchainUpdateCommand() : base("update") {
			this.Handler = this;
			this.AddCommand(new ToolchainUpdateCmakeCommand());
		}

		public Task<int> InvokeAsync(InvocationContext context) {
			throw new NotImplementedException();
		}

	}

}

