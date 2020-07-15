using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nsharp.PackageInfo {

	public class Package {

		public ICollection<Author>? Authors { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public Type Type { get; set; }

		[Required]
		public string Version { get; set; }

		public IEnumerable<ValidationResult> Validate() {
			var result = new List<ValidationResult>();

			if (this.Name == null) { result.Add(new ValidationResult($"{nameof(this.Name)} is null", new string[] { nameof(this.Name) })); }

			if (this.Type == Type.Undefined) { result.Add(new ValidationResult($"{nameof(this.Type)} is {nameof(Type.Undefined)}", new string[] { nameof(this.Type) })); }

			if (this.Version == null) { result.Add(new ValidationResult($"{nameof(this.Version)} is null", new string[] { nameof(this.Version) })); }

			return result;
		}

	}

}
