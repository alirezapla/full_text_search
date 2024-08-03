<<<<<<< HEAD
﻿namespace FullTextSearchApi.DataAccessLayer.Abstractions;

public interface IBaseRepository<T> where T:class
{
    Task GetAsync();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    
=======
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
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
}