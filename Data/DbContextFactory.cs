using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RestfulAPI.Models; 

// APPDBCONTEXTFACTORY CLASS THAT IMPLEMENTS IDESIGNTIMEDBONCTEXTFACTORY<APPDBCONTEXT> INTERFACE
// USED BY EFCORE TO CREATE THE DBCONTEXT INSTANCE FOR APPLYING MIGRATIONS AND UPDATING OUTSIDE
// RUNTIME
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    // Method for creating an instance of AppDbContext at design time, without needing the app to 
    // start
    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));

        return new AppDbContext(optionsBuilder.Options);
    }
}
