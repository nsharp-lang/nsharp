using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nsharp.Database;

namespace Nsharp.Commands {

	public class ToolchainUpdateCmakeCommand : Command, ICommandHandler {

		private readonly NsharpDbContext nsharpDbContext = new NsharpDbContext();
		private readonly Version Version = new Version(3, 17, 1);
		private readonly DirectoryInfo buildDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/cmake/build/");
		private readonly DirectoryInfo sourceDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/cmake/source/");

		public ToolchainUpdateCmakeCommand() : base("cmake") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			var cancellationToken = context.GetCancellationToken();

			var downloadResult = await this.DownloadAsync(cancellationToken);
			if (downloadResult != 0) { return downloadResult; }

			var configureResult = this.Configure();
			if (configureResult != 0) { return configureResult; }

			return 0;
		}

		private async Task<int> DownloadAsync(CancellationToken cancellationToken = default) {
			var gitPath = LibGit2Sharp.Repository.Init(this.sourceDirectoryInfo.FullName);
			using var repository = new LibGit2Sharp.Repository(gitPath, new LibGit2Sharp.RepositoryOptions { });
			using var remote = repository.Network.Remotes["origin"]
				?? repository.Network.Remotes.Add("origin", "https://github.com/Kitware/CMake.git");
			LibGit2Sharp.Commands.Fetch(repository, "origin", remote.FetchRefSpecs.Select(x => x.Specification), new LibGit2Sharp.FetchOptions { }, null);
			var tag = repository.Tags.FirstOrDefault(x => x.FriendlyName == $"v{this.Version.Major}.{this.Version.Minor}.{this.Version.Build}");
			var tagCommit = tag.PeeledTarget as LibGit2Sharp.Commit;
			LibGit2Sharp.Commands.Checkout(repository, tagCommit);
			repository.Reset(LibGit2Sharp.ResetMode.Hard, tagCommit);
			return 0;
		}

		private int Configure() {
			this.buildDirectoryInfo.Create();
			var processStartInfo = new ProcessStartInfo {
				Arguments = $"-DCMAKE_BUILD_TYPE=Release -S {this.sourceDirectoryInfo.FullName} -B {this.buildDirectoryInfo.FullName}",
				FileName = "cmake"
			};
			var process = System.Diagnostics.Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode;
		}

	}

}

