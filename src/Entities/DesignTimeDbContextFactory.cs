using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BtcEntities>
{
    //so we can run migrations from the class library -- borrowing appsettings.json from Site project is not optimal but not a big deal for the project size
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation

    private const string AppsettingsPath = "/../Site/appsettings.json";
    private const string ConnectionStringNamee = "DefaultConnection";

    public BtcEntities CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + AppsettingsPath).Build();
        var builder = new DbContextOptionsBuilder<BtcEntities>();
        builder.UseSqlServer(configuration.GetConnectionString(ConnectionStringNamee));
        return new BtcEntities(builder.Options);
    }
}