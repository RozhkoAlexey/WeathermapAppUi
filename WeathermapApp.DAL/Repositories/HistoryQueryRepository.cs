using Microsoft.EntityFrameworkCore;
using WeathermapApp.DAL.Models;
using WeathermapApp.DAL.Repositories.Interfaces;

namespace WeathermapApp.DAL.Repositories;

public class HistoryQueryRepository : BaseRepository<HistoryQueryModel>, IHistoryQueryRepository
{
    public HistoryQueryRepository(WeatherDbContext context) : base(context)
    { }

    public async Task<HistoryQueryModel?> SingleOrDefaultAsync(string zipCode, string countryName) =>
        await GetQuery()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ZipCode.Equals(zipCode) && x.CountryCode.Equals(countryName));
}
