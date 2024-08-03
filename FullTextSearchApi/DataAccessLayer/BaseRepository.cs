<<<<<<< HEAD
﻿using FullTextSearchApi.DataAccessLayer.Abstractions;

namespace FullTextSearchApi.DataAccessLayer;

public abstract class BaseRepository<T>: IBaseRepository<T> where T : class
{
    public  Task GetAsync()
    {
        throw new NotImplementedException();
    }

    public void Create(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
=======
using System.Linq.Expressions;
using FullTextSearchApi.Data;
using FullTextSearchApi.DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.DataAccessLayer;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly SearchDbContext Context;

    public BaseRepository(SearchDbContext context)
    {
        Context = context;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id) ?? throw new ArgumentException($"Entity {id} not found.");
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Queryable.Where(Context.Set<T>(), predicate).ToListAsync();
    }

    public async void AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async void AddRangeAsync(IEnumerable<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public async void RemoveAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async void RemoveRangeAsync(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
        await Context.SaveChangesAsync();
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
    }
}