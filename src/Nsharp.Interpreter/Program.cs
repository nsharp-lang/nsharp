using System;
using System.Threading.Tasks;

namespace Nsharp.Interpreter {

	public class Program {

		public static async Task Main(string[] args) {
			await Console.Out.WriteLineAsync("Hello World!");
		}

	}

}
