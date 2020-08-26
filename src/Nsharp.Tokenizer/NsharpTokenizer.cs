using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nsharp.Tokenizer {

	public class NsharpTokenizer {

		public static async IAsyncEnumerable<object> ParseTextAsync(Stream textStream) {
			yield return "";
		}

		public static async IAsyncEnumerable<object> ParseTextAsync(string text) {
			var bytes = Encoding.UTF8.GetBytes(text);
			await using var memoryStream = new MemoryStream(bytes);
			yield return NsharpTokenizer.ParseTextAsync(memoryStream);
		}

	}

}
