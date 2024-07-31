using FullTextSearchApi.Models;
using FullTextSearchApi.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.Data.Repositories;

public class InvertedIndexRepository : IInvertedIndexRepository
{
    protected DbSet<InvertedIndex> dbSet;

    private readonly IUnitOfwork _unitOfWork;

    public InvertedIndexRepository(IUnitOfwork unitOfwork)
    {
        _unitOfWork = unitOfwork;
        dbSet = _unitOfWork.Context.Set<InvertedIndex>();
    }


    public async Task<ActionResult<InvertedIndex>> Create(InvertedIndex invertedIndex)
    {
        dbSet.Add(invertedIndex);
        await _unitOfWork.SaveChangesAsync();
        return invertedIndex;
    }

    public async Task<List<String>> Get(String query)
    {
        var data = await dbSet.Where(x => x.Word == query.ToUpper()).Select(x => x.DocumentId).ToListAsync();
        return data;
    }

    public async Task<List<String>> ConditionalGet(
        HashSet<String> allWords, HashSet<String> anyWords, HashSet<String> excludeWords)
    {
        var query = dbSet
            .Where(wd => allWords.Contains(wd.Word) || anyWords.Contains(wd.Word) || excludeWords.Contains(wd.Word))
            .GroupBy(wd => wd.DocumentId)
            .Where(g => (!allWords.Any() || allWords.All(word => g.Any(wd => wd.Word == word))) &&
                        (!anyWords.Any() || g.Any(wd => anyWords.Contains(wd.Word))) &&
                        (!excludeWords.Any() || !g.Any(wd => excludeWords.Contains(wd.Word))))
            .Select(g => g.Key);
        return await query.ToListAsync();
    }


    public async void Delete(int id)
    {
        var data = await dbSet.FindAsync(id);
        if (data != null)
        {
            dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}