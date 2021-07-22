using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nsharp.Toolchain
{

	public class CmakeToolchain
    {

		private readonly string hash = "56d677070644a403417a286cceca39e3d240394c347a6503215889c3a55ceccbc0250b4fab76d963430496ed6195302c08b2114d57f7e37beba674818cbbbfd9";
		private readonly string directoryHash = "5488eceb93c210b6c8bda4e84a6ea109eb5a110e4700cf06e8a323613bf6d94244d1b9114c05e7f97bcc3feb3e4307869c7d019d72da92b2b780d9509f5fee10";
		private readonly Uri uri = new Uri("https://github.com/Kitware/CMake/releases/download/v3.21.0/cmake-3.21.0.zip");
		private readonly FileInfo zipFileInfo = new FileInfo($"{Path.GetTempPath()}/nsharp-cmake.zip");
		private readonly DirectoryInfo directoryInfo = new DirectoryInfo($"{Path.GetTempPath()}/nsharp-cmake/");

		public async Task DownloadAsync(CancellationToken cancellationToken = default) {
			if (this.zipFileInfo.Exists)
			{
				await using var zipReadFileStream = this.zipFileInfo.OpenRead();
				using var sha512 = SHA512.Create();
				var hashResult = await sha512.ComputeHashAsync(zipReadFileStream, cancellationToken);
				var hashResultString = BitConverter.ToString(hashResult).Replace("-", "").ToLower();
				if(hashResultString == this.hash) { return; }
				await zipReadFileStream.DisposeAsync();
				zipFileInfo.Delete();
			}
			using var zipWriteFileStream = this.zipFileInfo.OpenWrite();
			using var httpClient = new HttpClient();
			using var httpResponseMessage = await httpClient.GetAsync(this.uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			await httpResponseMessage.Content.CopyToAsync(zipWriteFileStream, cancellationToken);
		}

		public async Task ExtractAsync(CancellationToken cancellationToken = default)
		{
			if (this.directoryInfo.Exists) {
				var fileInfos = Directory.GetFiles(this.directoryInfo.FullName, "*", SearchOption.AllDirectories)
					.OrderBy(x => x)
					.Select(x=> new FileInfo(x))
					.ToArray();
				using var sha512 = SHA512.Create();
				await using var cryptoStram = new CryptoStream(Stream.Null, sha512, CryptoStreamMode.Write);
				foreach (var fileInfo in fileInfos) {
					var relativeFileName = Path.GetRelativePath(directoryInfo.FullName, fileInfo.FullName);
					var relativeFileNameBytes = Encoding.UTF8.GetBytes(relativeFileName);
					await cryptoStram.WriteAsync(relativeFileNameBytes, cancellationToken);

					await using var fileInfoFileStream = fileInfo.OpenRead();
					await fileInfoFileStream.CopyToAsync(cryptoStram, cancellationToken);
				}
				cryptoStram.FlushFinalBlock();
				var hashResult = sha512.Hash;
				var hashResultString = BitConverter.ToString(hashResult).Replace("-", "").ToLower();
				if (hashResultString == this.directoryHash) { return; }
				this.directoryInfo.Delete();
			}
			await using var fileStream = this.zipFileInfo.OpenRead();
			using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read);
			zipArchive.ExtractToDirectory(this.directoryInfo.FullName, true);
		}

    }

}
