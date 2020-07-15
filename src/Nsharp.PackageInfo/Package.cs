using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nsharp.PackageInfo {

	public class Package : IValidatableObject {

		public ICollection<Author>? Authors { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public Type Type { get; set; }

		[Required]
		public string Version { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			throw new NotImplementedException();
		}

	}

}
