using System.CommandLine;

namespace NSharp.Commands {

	public class NSharpRootCommand : RootCommand{

		public NSharpRootCommand() {
			this.AddCommand(new RestoreCommand());
			this.AddCommand(new BuildCommand());
		}

	}

}
