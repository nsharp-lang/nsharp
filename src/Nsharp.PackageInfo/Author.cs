using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Author : IValidatableObject {

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		public string? Name { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			var validationResults = new List<ValidationResult>();

			Validator.TryValidateProperty(
				this.Email,
				new ValidationContext(this, null, null) { MemberName = nameof(this.Email) },
				validationResults
			);

			Validator.TryValidateProperty(
				this.Name,
				new ValidationContext(this, null, null) { MemberName = nameof(this.Name) },
				validationResults
			);

			return validationResults;
		}

	}

}
