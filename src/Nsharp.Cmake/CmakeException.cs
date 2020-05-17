using System;

namespace Nsharp.Cmake {

	public class CmakeException : Exception {

		public int ExitCode { get; set; }

		public CmakeException(int exitCode) {
			this.ExitCode = exitCode;
		}

	}

}
