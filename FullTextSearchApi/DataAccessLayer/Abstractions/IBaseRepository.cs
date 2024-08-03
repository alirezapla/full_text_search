using System.Linq.Expressions;

namespace FullTextSearchApi.DataAccessLayer.Abstractions;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    void AddAsync(T entity);
    void AddRangeAsync(IEnumerable<T> entities);
    void RemoveAsync(T entity);
    void RemoveRangeAsync(IEnumerable<T> entities);
}