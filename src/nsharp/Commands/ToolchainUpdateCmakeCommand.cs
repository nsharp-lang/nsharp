using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nsharp.Cmake;
using Nsharp.Database;

namespace Nsharp.Commands {

	public class ToolchainUpdateCmakeCommand : Command, ICommandHandler {

		private readonly NsharpDbContext nsharpDbContext = new NsharpDbContext();
		private readonly Version Version = new Version(3, 17, 2);
		private readonly DirectoryInfo buildDirectoryInfo = new DirectoryInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nsharp/toolchain/cmake/build/");
		private readonly DirectoryInfo installDirectoryInfo = new DirectoryInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nsharp/toolchain/cmake/install/");
		private readonly DirectoryInfo sourceDirectoryInfo = new DirectoryInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nsharp/toolchain/cmake/source/");

		public ToolchainUpdateCmakeCommand() : base("cmake") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			var sourceResult = this.Source();
			if (sourceResult != 0) { return sourceResult; }

			var configureResult = this.Configure();
			if (configureResult != 0) { return configureResult; }

			var buildResult = this.Build();
			if (buildResult != 0) { return buildResult; }

			var installResult = this.Install();
			if (installResult != 0) { return installResult; }

			return 0;
		}

		private int Build() {
			var cmakeBuildOptions = new CmakeBuildOptions {
				BuildDirectory = this.buildDirectoryInfo,
				Config = "Release"
			};
			CmakeCommands.Build(cmakeBuildOptions);
			return 0;
		}

		private int Configure() {
			var cmakeConfigureOptions = new CmakeConfigureOptions {
				BuildDirectory = this.buildDirectoryInfo,
				BuildType = "Release",
				SourceDirectory = this.sourceDirectoryInfo
			};
			CmakeCommands.Configure(cmakeConfigureOptions);
			return 0;
		}

		private int Install() {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"--install", $"{this.buildDirectoryInfo.FullName}",
					"--prefix", $"{this.installDirectoryInfo.FullName}",
				},
				FileName = "cmake"
			};
			var process = System.Diagnostics.Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode;
		}

		private int Source() {
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

	}

}

