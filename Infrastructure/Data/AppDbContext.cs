using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
			try
			{
				var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
				if (databaseCreator != null)
				{
					if (!databaseCreator.CanConnect()) databaseCreator.Create();
					if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
        }

		//public DbSet<Student> Students { get; set; }
    }
}
