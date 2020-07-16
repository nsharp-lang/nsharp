using Xunit;

namespace Nsharp.PackageInfo.Tests {

	public class AuthorTests {

		[Fact]
		public void Fact1() {
			var author = new Author {
				Email = "deinok@deinok.com",
				Name = "deinok"
			};
			Assert.Empty(author.Validate());
		}

		[Fact]
		public void Fact2() {
			var author = new Author {
				Email = "deinok@deinok.com",
			};
			Assert.Empty(author.Validate());
		}

		[Fact]
		public void Fact3() {
			var author = new Author {
			};
			Assert.NotEmpty(author.Validate());
		}

		[Fact]
		public void Fact4() {
			var author = new Author {
				Name = "deinok"
			};
			Assert.NotEmpty(author.Validate());
		}

		[Fact]
		public void Fact5() {
			var author = new Author {
				Email = "@deinok.com",
				Name = "deinok"
			};
			Assert.NotEmpty(author.Validate());
		}

		[Fact]
		public void Fact6() {
			var author = new Author {
				Email = "deinok.com",
				Name = "deinok"
			};
			Assert.NotEmpty(author.Validate());
		}

		[Fact]
		public void Fact7() {
			var author = new Author {
				Email = "deinok@",
				Name = "deinok"
			};
			Assert.NotEmpty(author.Validate());
		}

		[Fact]
		public void Fact8() {
			var author = new Author {
				Email = "deinok",
				Name = "deinok"
			};
			Assert.NotEmpty(author.Validate());
		}

	}

}
