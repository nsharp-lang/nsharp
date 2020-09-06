using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Nsharp.AbstractSyntaxTree.Internal;

namespace Nsharp.AbstractSyntaxTree {

	public class SyntaxTree {

		public static async ValueTask<SyntaxTree> ParseAsync(string text, CancellationToken cancellationToken = default) {
			var stringReader = new StringReader(text);
			return await ParseAsync(stringReader, cancellationToken);
		}

		public static async ValueTask<SyntaxTree> ParseAsync(Stream stream, CancellationToken cancellationToken = default) {
			var streamReader = new StreamReader(stream);
			return await ParseAsync(streamReader, cancellationToken);
		}

		public static async ValueTask<SyntaxTree> ParseAsync(TextReader textReader, CancellationToken cancellationToken = default) {
			var tokens = NsharpTokenizer.TokenizeAsync(textReader);
			return null;
		}

	}

}
