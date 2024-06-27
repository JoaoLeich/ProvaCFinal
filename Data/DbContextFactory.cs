using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class LojaDBContextFactory : IDesignTimeDbContextFactory<LojaDBContext>
{



    public LojaDBContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<LojaDBContext>();

        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var ConnectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 26)));

        return new LojaDBContext(optionsBuilder.Options);
    }

}