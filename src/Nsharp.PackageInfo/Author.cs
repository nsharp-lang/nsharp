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

			validationContext.MemberName = nameof(this.Email);
			Validator.TryValidateProperty(this.Email, validationContext, validationResults);

			validationContext.MemberName = nameof(this.Name);
			Validator.TryValidateProperty(this.Name, validationContext, validationResults);

			return validationResults;
		}

	}

}
