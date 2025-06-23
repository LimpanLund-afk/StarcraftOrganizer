using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StarcraftOrganizer.Data.DataContext
{


    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true);

            // Lägg till User Secrets bara om de finns
            //try
            //{
            //    //configBuilder.AddUserSecrets<Program>();
            //}
            //catch
            //{
            //    // Ignorera om User Secrets inte finns/är tomma
            //}

            var config = configBuilder.Build();

            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                ?? config.GetConnectionString("PostgreSQL")
                ?? config.GetConnectionString("SQL");

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new DataContext(optionsBuilder.Options);
        }
    }
}
