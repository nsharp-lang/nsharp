using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nsharp.Database;
using Nsharp.Git;

namespace Nsharp.Commands {

	public class ToolchainUpdateLlvmCommand : Command, ICommandHandler {

		private readonly NsharpDbContext nsharpDbContext = new NsharpDbContext();
		private readonly Version version = new Version(10, 0, 0);
		private readonly DirectoryInfo buildDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/llvm/build/");
		private readonly DirectoryInfo sourceDirectoryInfo = new DirectoryInfo($"{Path.GetTempPath()}nsharp/llvm/source/");
		private readonly Uri gitUri = new Uri("https://github.com/llvm/llvm-project.git");

		public ToolchainUpdateLlvmCommand() : base("llvm") {
			this.Handler = this;
		}

		public async Task<int> InvokeAsync(InvocationContext context) {
			var downloadResult = this.Download();
			if (downloadResult != 0) { return downloadResult; }

			var configureResult = this.Configure();
			if (configureResult != 0) { return configureResult; }

			var buildResult = this.Build();
			if (buildResult != 0) { return buildResult; }

			return await Task.FromResult(0);
		}

		private int Download() {
			var repository = new Repository(this.sourceDirectoryInfo);

			var initResult = repository.Init();

			var remoteAddResult = repository.RemoteAdd("https://github.com/llvm/llvm-project.git");

			var fetchResult = repository.Fetch();

			var checkoutResult = repository.Checkout($"llvmorg-{this.version.Major}.{this.version.Minor}.{this.version.Build}");

			return 0;
		}

		private int Build() {
			this.buildDirectoryInfo.Create();
			var processStartInfo = new ProcessStartInfo {
				Arguments = $"--build {this.buildDirectoryInfo.FullName}",
				FileName = "cmake"
			};
			var process = System.Diagnostics.Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode;
		}

		private int Configure() {
			this.buildDirectoryInfo.Create();
			var processStartInfo = new ProcessStartInfo {
				Arguments = $"-DCMAKE_BUILD_TYPE=Release -S {this.sourceDirectoryInfo.FullName}llvm/ -B {this.buildDirectoryInfo.FullName}",
				FileName = "cmake"
			};
			var process = System.Diagnostics.Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode;
		}

	}

}

