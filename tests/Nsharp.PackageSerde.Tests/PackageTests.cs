using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Nsharp.PackageSerde.Tests {

	public class PackageTests {

		[Fact]
		public void Fact1() {
			var package = new Package {
				Name = "package-name",
				Type = PackageType.Executable,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.Empty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact2() {
			var package = new Package {
				Name = "package-name",
				Type = PackageType.Library,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.Empty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact3() {
			var package = new Package {
				Name = "package-name",
				Type = PackageType.Undefined,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.NotEmpty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact4() {
			var package = new Package {
				Type = PackageType.Executable,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.NotEmpty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact5() {
			var package = new Package {
				Name = "package-name",
				Type = PackageType.Executable,
			};
			var validationContext = new ValidationContext(package);
			Assert.NotEmpty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact6() {
			var package = new Package {
				Authors = new[]{
					new Author { Email = "deinok@deinok.com" }
				},
				Name = "package-name",
				Type = PackageType.Executable,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.Empty(package.Validate(validationContext));
		}

		[Fact]
		public void Fact7() {
			var package = new Package {
				Authors = new[]{
					new Author { Email = "@deinok.com" }
				},
				Name = "package-name",
				Type = PackageType.Executable,
				Version = "1.0.0-alpha1",
			};
			var validationContext = new ValidationContext(package);
			Assert.NotEmpty(package.Validate(validationContext));
		}

	}

}
