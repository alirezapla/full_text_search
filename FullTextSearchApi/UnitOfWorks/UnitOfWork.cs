namespace FullTextSearchApi.UnitOfWorks;

using Microsoft.EntityFrameworkCore;
using FullTextSearchApi.Data;

public class UnitOfwork : IUnitOfwork
{
    private readonly SearchDbContext _context;
    private bool _disposed = false;

    public UnitOfwork(SearchDbContext context)
    {
        _context = context;
    }
    public DbContext Context => _context;

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
    
    
}