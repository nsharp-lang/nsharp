using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nsharp.PackageInfo {

	public class Author {

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		public string? Name { get; set; }

		public IEnumerable<ValidationResult> Validate() {
			var emailAddressAttribute = new EmailAddressAttribute();
			var requiredAttribute = new RequiredAttribute();

			var result = new List<ValidationResult>();

			if (!requiredAttribute.IsValid(this.Email)) { result.Add(new ValidationResult($"{nameof(this.Email)} is null", new string[] { nameof(this.Email) })); }
			if (requiredAttribute.IsValid(this.Email) && !emailAddressAttribute.IsValid(this.Email)) { result.Add(new ValidationResult($"{nameof(this.Email)} is not valid", new string[] { nameof(this.Email) })); }

			return result;
		}

	}

}
