using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Nsharp.AbstractSyntaxTree.Internal;

namespace Nsharp.AbstractSyntaxTree {

	public class SyntaxTree {

		public static SyntaxTree Parse(string text) {
			return null;
		}

		public static SyntaxTree Parse(Stream stream) {
			return null;
		}

		public static async ValueTask<SyntaxTree> ParseAsync(TextReader textReader, CancellationToken cancellationToken = default) {
			var tokens = NsharpTokenizer.TokenizeAsync(textReader);
			return null;
		}

	}

}
