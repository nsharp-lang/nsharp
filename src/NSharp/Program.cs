using System.CommandLine.Invocation;
using System.Threading.Tasks;
using NSharp.Commands;

namespace NSharp {

    public class Program{

        public static async Task<int> Main(string[] args){
            var nSharpRootCommand = new NSharpRootCommand {
            };
            return await nSharpRootCommand.InvokeAsync(args);
        }

    }

}
