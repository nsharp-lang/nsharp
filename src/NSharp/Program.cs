using System.CommandLine;
using System.Threading.Tasks;
using Nsharp.Commands;

namespace Nsharp {

	public class Program {

		public static async Task<int> Main(string[] args) {
			var nsharpRootCommand = new NSharpRootCommand {
			};
			return await nsharpRootCommand.InvokeAsync(args);
		}

	}

}
