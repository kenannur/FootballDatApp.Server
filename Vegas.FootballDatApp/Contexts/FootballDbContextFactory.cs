using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vegas.FootballDatApp.Contexts
{
    /// <summary>
    /// "dotnet ef migrations add 'MigrationName'" komutu çalıştırıldığında
    /// design time'da bu kodu okuyarak migration oluşturulmasını sağlar.
    /// </summary>
    public class FootballDbContextFactory : IDesignTimeDbContextFactory<FootballDbContext>
    {
        public FootballDbContext CreateDbContext(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<FootballDbContext>();
            dbContextOptionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("Default"));

            return new FootballDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
