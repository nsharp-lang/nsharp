using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Nsharp.Commands {

	public class RestoreCommand : Command, ICommandHandler {

		public RestoreCommand() : base("restore") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			return await Task.FromResult(0);
		}

	}

}
