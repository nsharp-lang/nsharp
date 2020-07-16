using System;
using System.Threading.Tasks;

namespace Nsharp.Interpreter {

	public class Program {

		public static async Task<int> Main(string[] args) {
			Console.WriteLine("Hello World!");
			return await Task.FromResult(0);
		}

	}

}
