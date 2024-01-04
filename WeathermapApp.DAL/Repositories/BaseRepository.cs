using Microsoft.EntityFrameworkCore;
using WeathermapApp.DAL.Models;
using WeathermapApp.DAL.Repositories.Interfaces;

namespace WeathermapApp.DAL.Repositories;

public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel, new()
{
    protected WeatherDbContext Context { get; }
    protected DbSet<TModel> DbSet { get; }

    public BaseRepository(WeatherDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TModel>();
    }

    public virtual async Task CreateAsync(params TModel[] items)
    {
        await DbSet.AddRangeAsync(items);
        await Context.SaveChangesAsync();

        foreach (var item in items)
        {
            Context.Entry(item).State = EntityState.Detached;
        }
    }

    public virtual async Task UpdateAsync(params TModel[] item)
    {
        Context.UpdateRange(item);
        await Context.SaveChangesAsync();

        foreach (var baseModel in item)
        {
            Context.Entry(baseModel).State = EntityState.Detached;
        }
    }

    public async Task<TModel?> FindByIdAsync(Guid id) => await GetQuery().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<TModel>> GetAllAsync() => await GetQuery().AsNoTracking().ToArrayAsync();

    public virtual IQueryable<TModel> GetQuery() => DbSet.AsQueryable();

    public virtual async Task RemoveAsync(Guid id)
    {
        var model = DbSet.FirstOrDefault(x => x.Id == id);

        if (model is null)
        {
            return;
        }

        DbSet.Remove(model);
        await Context.SaveChangesAsync();
    }
}