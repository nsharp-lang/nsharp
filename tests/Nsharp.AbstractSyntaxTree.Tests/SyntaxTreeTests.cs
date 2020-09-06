using System;
using System.Threading.Tasks;
using Xunit;

namespace Nsharp.AbstractSyntaxTree {

	public class SyntaxTreeTests {

		[Fact]
		public async Task Fact1() {
			SyntaxTree.ParseAsync("hola caracola;");
		}

	}

}
