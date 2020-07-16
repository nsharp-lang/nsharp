using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Package {

		public ICollection<Author>? Authors { get; set; }

		public BuildOptions? BuildOptions { get; set; }

		public Dependencies? Dependencies { get; set; }

		public Licence? Licence { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Readme { get; set; }

		public Uri? Repository { get; set; }

		public ICollection<Target>? Targets { get; set; }

		[Required]
		public PackageType Type { get; set; }

		[Required]
		public string Version { get; set; }

		public IEnumerable<ValidationResult> Validate() {
			var requiredAttribute = new RequiredAttribute();

			var result = new List<ValidationResult>();

			foreach (var author in this.Authors ?? Array.Empty<Author>()) {
				result.AddRange(author.Validate());
			}

			if (!requiredAttribute.IsValid(this.Name)) { result.Add(new ValidationResult($"{nameof(this.Name)} is null", new string[] { nameof(this.Name) })); }

			if (!requiredAttribute.IsValid(this.Type)) { result.Add(new ValidationResult($"{nameof(this.Type)} is {nameof(PackageType.Undefined)}", new string[] { nameof(this.Type) })); }

			if (!requiredAttribute.IsValid(this.Version)) { result.Add(new ValidationResult($"{nameof(this.Version)} is null", new string[] { nameof(this.Version) })); }

			return result;
		}

	}

}
