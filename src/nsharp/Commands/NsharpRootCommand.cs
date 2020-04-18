using System.CommandLine;

namespace Nsharp.Commands {

	public class NsharpRootCommand : RootCommand {

		public NsharpRootCommand() {
			this.AddCommand(new RestoreCommand());
			this.AddCommand(new BuildCommand());
			this.AddCommand(new ToolchainCommand());
		}

	}

}
