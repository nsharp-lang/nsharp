using System.CommandLine;

namespace Nsharp.Commands {

	public class ToolchainCommand : Command {

		public ToolchainCommand() : base("toolchain") {
			this.AddCommand(new ToolchainUpdateCommand());
		}

	}

}
