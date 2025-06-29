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
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var config = configBuilder.Build();

            var connectionString = config.GetConnectionString("DB_CONNECTION_STRING")
                                   ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                   ?? throw new InvalidOperationException("Ingen giltig databasanslutning hittades i DataContextFactory");

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
