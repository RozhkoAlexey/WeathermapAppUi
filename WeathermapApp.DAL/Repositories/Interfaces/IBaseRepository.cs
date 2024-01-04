using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL.Repositories.Interfaces;

public interface IBaseRepository<TModel> where TModel : BaseModel
{
    Task CreateAsync(params TModel[] item);
    Task UpdateAsync(params TModel[] item);
    Task<TModel?> FindByIdAsync(Guid id);
    Task<IEnumerable<TModel>> GetAllAsync();
    IQueryable<TModel> GetQuery();
    Task RemoveAsync(Guid id);
}
