using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Nsharp.Database;

namespace Nsharp.Commands {

	public class ToolchainUpdateCommand : Command, ICommandHandler {

		private readonly NsharpDbContext nsharpDbContext = new NsharpDbContext();

		public ToolchainUpdateCommand() : base("update") {
			this.Handler = this;
		}

		public Task<int> InvokeAsync(InvocationContext context) {
			throw new NotImplementedException();
		}

	}

}

