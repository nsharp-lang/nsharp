using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Nsharp.PackageSerde.Tests {

	public class AuthorTests {

		[Fact]
		public void Fact1() {
			var author = new Author {
				Email = "deinok@deinok.com",
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.Empty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact2() {
			var author = new Author {
				Email = "deinok@deinok.com",
			};
			var validationContext = new ValidationContext(author);
			Assert.Empty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact3() {
			var author = new Author {
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact4() {
			var author = new Author {
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact5() {
			var author = new Author {
				Email = "@deinok.com",
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact6() {
			var author = new Author {
				Email = "deinok.com",
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact7() {
			var author = new Author {
				Email = "deinok@",
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

		[Fact]
		public void Fact8() {
			var author = new Author {
				Email = "deinok",
				Name = "deinok"
			};
			var validationContext = new ValidationContext(author);
			Assert.NotEmpty(author.Validate(validationContext));
		}

	}

}
