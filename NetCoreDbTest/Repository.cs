using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NetCoreDbTest
{
    public class Repository : IDesignTimeDbContextFactory<HangfireContext>
    {
        private static string _connectionString;

        public HangfireContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public HangfireContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<HangfireContext>();
            builder.UseSqlServer(_connectionString);

            return new HangfireContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("HangfireReadOnly");
        }
    }
}
