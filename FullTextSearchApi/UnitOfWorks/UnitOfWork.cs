using FullTextSearchApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.UnitOfWorks;

public sealed class UnitOfWork(SearchDbContext context) : IUnitOfWork
{
    private bool _disposed = false;

    public DbContext Context => context;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }
    
    
}