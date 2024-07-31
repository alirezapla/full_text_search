using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.UnitOfWorks;

public interface IUnitOfwork : IDisposable
{
    DbContext Context { get; }
    public Task SaveChangesAsync();
    
}