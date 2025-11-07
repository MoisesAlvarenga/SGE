using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SGE.Data;

namespace SGE.Repository;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<T> _set;

    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
        _set = _ctx.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _set.AddAsync(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var prop = typeof(T).GetProperty("Id");
        if (prop == null)
            throw new InvalidOperationException("No Id property");
        var entity = await _set.FindAsync(id);
        if (entity != null)
        {
            _set.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<T>> FindAllAsync() => await _set.ToListAsync();

    public async Task<T?> FindByIdAsync(Guid id) => await _set.FindAsync(id);

    public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
    {
        return await _set.Where(predicate).ToListAsync();
    }


    public async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _ctx.SaveChangesAsync();
    }
}
