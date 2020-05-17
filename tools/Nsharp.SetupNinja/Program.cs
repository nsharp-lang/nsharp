using System;
using System.IO;
using System.Linq;
using Nsharp.Cmake;
using Nsharp.Ninja;

namespace Nsharp.SetupNinja {

	public class Program {

		private static DirectoryInfo BuildDirectoryInfo { get; } = new DirectoryInfo($"{AppContext.BaseDirectory}ninja/build/");
		private static DirectoryInfo InstallDirectoryInfo { get; } = new DirectoryInfo($"{AppContext.BaseDirectory}ninja/install/");
		private static DirectoryInfo SourceDirectoryInfo { get; } = new DirectoryInfo($"{AppContext.BaseDirectory}ninja/source/");

		private static Version Version { get; } = new Version(1, 10, 0);

		public static void Main(string[] args) {
			Source();
			Configure();
			Build();
			Install();
		}

		private static void Build() {
			CmakeCommands.Build(new CmakeBuildOptions {
				BuildDirectory = BuildDirectoryInfo,
				Config = "Release",
				Parallel = (uint)Environment.ProcessorCount,
			});
		}

		private static void Configure() {
			CmakeCommands.Configure(new CmakeConfigureOptions {
				BuildDirectory = BuildDirectoryInfo,
				BuildType = "Release",
				Generator = "Ninja",
				SourceDirectory = SourceDirectoryInfo
			});
		}

		private static void Install() {
			CmakeCommands.Install(new CmakeInstallOptions {
				BuildDirectory = BuildDirectoryInfo,
				Prefix = InstallDirectoryInfo
			});
		}

		private static void Source() {
			var gitPath = LibGit2Sharp.Repository.Init(SourceDirectoryInfo.FullName);
			using var repository = new LibGit2Sharp.Repository(gitPath, new LibGit2Sharp.RepositoryOptions { });
			using var remote = repository.Network.Remotes["origin"]
				?? repository.Network.Remotes.Add("origin", "https://github.com/ninja-build/ninja.git");
			LibGit2Sharp.Commands.Fetch(repository, "origin", remote.FetchRefSpecs.Select(x => x.Specification), new LibGit2Sharp.FetchOptions { }, null);
			var tag = repository.Tags.FirstOrDefault(x => x.FriendlyName == $"v{Version.Major}.{Version.Minor}.{Version.Build}");
			var tagCommit = tag.PeeledTarget as LibGit2Sharp.Commit;
			LibGit2Sharp.Commands.Checkout(repository, tagCommit);
			repository.Reset(LibGit2Sharp.ResetMode.Hard, tagCommit);
		}

	}

}
