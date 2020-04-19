using System.Diagnostics;
using System.IO;

namespace Nsharp.Git {

	public class Repository {

		public DirectoryInfo Path { get; set; }

		public Repository(DirectoryInfo path) {
			this.Path = path;
		}

		public bool Checkout(string refspec, bool progress = true) {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"checkout",
					"--progress",
					refspec
				},
				FileName = "git",
				WorkingDirectory = this.Path.FullName
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode == 0;
		}

		public bool Fetch(uint depth = 1, bool progress = true, bool prune=true, bool recurseModules = true) {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"fetch",
					$"--depth={depth}",
					"--progress",
					"--prune",
					"--recurse-submodules=yes"
				},
				FileName = "git",
				WorkingDirectory = this.Path.FullName
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode == 0;
		}

		public bool Init() {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"init",
					this.Path.FullName
				},
				FileName = "git"
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode == 0;
		}

		public bool RemoteAdd(string remoteUri) {
			var processStartInfo = new ProcessStartInfo {
				ArgumentList = {
					"remote",
					"add",
					"origin",
					remoteUri,
				},
				FileName = "git",
				WorkingDirectory = this.Path.FullName
			};
			using var process = Process.Start(processStartInfo);
			process.WaitForExit();
			return process.ExitCode == 0 || process.ExitCode == 128;
		}

	}

}
