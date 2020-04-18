using System.CommandLine;
using System.Threading.Tasks;
using Nsharp.Commands;

namespace Nsharp {

	public class Program {

		public static async Task<int> Main(string[] args) {
			args = new string[] { "toolchain" };
			var nsharpRootCommand = new NsharpRootCommand {
			};
			return await nsharpRootCommand.InvokeAsync(args);
		}

	}

}
