using System;
using System.Threading.Tasks;

namespace Nsharp.Compiler {

	public class Program {

		public static async Task<int> Main(string[] args) {
			Console.WriteLine("Hello World!");
			return await Task.FromResult(0);
		}

	}

}
