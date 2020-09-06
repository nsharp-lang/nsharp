using System;
using System.Threading.Tasks;
using Xunit;

namespace Nsharp.AbstractSyntaxTree {

	public class SyntaxTreeTests {

		[Fact]
		public async Task Fact1() {
			var a=await SyntaxTree.ParseAsync("hola caracola;");
		}

	}

}
