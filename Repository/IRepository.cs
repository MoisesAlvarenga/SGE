using System.Linq.Expressions;

namespace SGE.Repository;

public interface IRepository<T>
    where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> FindAllAsync();
    Task<T?> FindByIdAsync(Guid id);
    Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
