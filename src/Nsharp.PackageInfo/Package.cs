using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Package : IValidatableObject {

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

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			var validationResults = new List<ValidationResult>();

			validationContext.MemberName = nameof(this.Name);
			Validator.TryValidateProperty(this.Name, validationContext, validationResults);

			validationContext.MemberName = nameof(this.Type);
			Validator.TryValidateProperty(this.Type, validationContext, validationResults);

			validationContext.MemberName = nameof(this.Version);
			Validator.TryValidateProperty(this.Version, validationContext, validationResults);

			return validationResults;
		}

	}

}
