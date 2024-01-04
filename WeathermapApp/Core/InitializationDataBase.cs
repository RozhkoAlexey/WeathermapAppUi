using Microsoft.EntityFrameworkCore;
using WeathermapApp.DAL;

namespace WeathermapApp.Core;

public class InitializationDataBase
{
    private const string InitDb = "INIT_DB";

    public static void Init(IHost host)
    {
        var config = host.Services.GetRequiredService<IConfiguration>();

        if (config[InitDb] != "true")
        {
            return;
        }

        using var scope = host.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<InitializationDataBase>>();

        logger.LogInformation("Database rebuild started.");

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        dbContext.SaveChanges();

        logger.LogInformation("Database rebuild finished.");
    }
}