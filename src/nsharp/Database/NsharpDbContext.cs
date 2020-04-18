using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Nsharp.Database {

	public class NsharpDbContext : DbContext {

		private static readonly FileInfo dbContextFileInfo = new FileInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nsharp/nsharp.sqlite3");

		private static readonly DbContextOptionsBuilder<NsharpDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<NsharpDbContext>()
			.UseSqlite($"Data Source={dbContextFileInfo.FullName}");

		public NsharpDbContext() : base(dbContextOptionsBuilder.Options) {
			NsharpDbContext.dbContextFileInfo.Directory.Create();
		}

	}

}
