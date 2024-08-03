using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    DbContext Context { get; }
    public Task SaveChangesAsync();
    
}