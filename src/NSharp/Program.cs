using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace NSharp {

    public class Program{

        public static async Task<int> Main(string[] args){
            var rootCommand = new RootCommand { };
            return await rootCommand.InvokeAsync(args);
        }

    }

}
