using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Toolchain
{

	public class CmakeToolchain
    {

		private readonly string hash = "56d677070644a403417a286cceca39e3d240394c347a6503215889c3a55ceccbc0250b4fab76d963430496ed6195302c08b2114d57f7e37beba674818cbbbfd9";
		private readonly Uri uri = new Uri("https://github.com/Kitware/CMake/releases/download/v3.21.0/cmake-3.21.0.zip");
		private readonly FileInfo zipFileInfo= new FileInfo($"{Path.GetTempPath()}/cmake.zip");

		public async Task DownloadAsync(CancellationToken cancellationToken = default) {
			if (this.zipFileInfo.Exists)
			{
				await using var zipReadFileStream = this.zipFileInfo.OpenRead();
				using var sha512 = SHA512.Create();
				var hashResult = await sha512.ComputeHashAsync(zipReadFileStream, cancellationToken);
				var hashResultString = BitConverter.ToString(hashResult).Replace("-", "").ToLower();
				if(hashResultString == hash) { return; }
			}
			using var zipWriteFileStream = zipFileInfo.OpenWrite();
			using var httpClient = new HttpClient();
			using var httpResponseMessage = await httpClient.GetAsync(this.uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			await httpResponseMessage.Content.CopyToAsync(zipWriteFileStream, cancellationToken);
		}

    }

}
