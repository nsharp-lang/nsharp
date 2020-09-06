using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Nsharp.AbstractSyntaxTree.Internal {

	internal static class NsharpTokenizer {

		public static async IAsyncEnumerable<string> TokenizeAsync(TextReader textReader, CancellationToken cancellationToken = default) {
			using var memoryOwner = MemoryPool<char>.Shared.Rent(1024);
			var memory = memoryOwner.Memory;

			var readResult = 0;
			do {
				readResult = await textReader.ReadAsync(memory, cancellationToken);
				var slicedMemory = memory.Slice(0, readResult);
				if(slicedMemory.IsEmpty){ yield break; }

				// read the slice
			} while (readResult != 0);
			yield break;
		}

	}

}
