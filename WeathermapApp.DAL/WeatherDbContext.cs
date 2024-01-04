using Microsoft.EntityFrameworkCore;
using WeathermapApp.DAL.Configurations;
using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL;

public class WeatherDbContext : DbContext
{
    public DbSet<HistoryQueryModel> HistoryQueries { get; set; }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new HistoryQueryModelConfiguration());
    }
}
