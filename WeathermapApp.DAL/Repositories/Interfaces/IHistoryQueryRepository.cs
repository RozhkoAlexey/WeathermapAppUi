using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL.Repositories.Interfaces;

public interface IHistoryQueryRepository : IBaseRepository<HistoryQueryModel>
{
    public Task<HistoryQueryModel?> SingleOrDefaultAsync(string zipCode, string countryName);
}
