using FullTextSearchApi.Data;
using FullTextSearchApi.DataAccessLayer;
using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.Repositories;

public class InvertedIndexRepository : BaseRepository<InvertedIndex>, IInvertedIndexRepository
{
    public InvertedIndexRepository(SearchDbContext context) : base(context)
    {
    }

    public async Task<List<String>> Get(String query)
    {
        var data = await Context.InvertedIndex.Where(x => x.Word == query.ToUpper())
            .Select(x => x.DocumentId)
            .ToListAsync();
        return data;
    }

    public async Task<List<String>> ConditionalGet(
        HashSet<String> allWords, HashSet<String> anyWords, HashSet<String> excludeWords)
    {
        var query = Context.InvertedIndex
            .Where(wd => allWords.Contains(wd.Word) || anyWords.Contains(wd.Word) || excludeWords.Contains(wd.Word))
            .GroupBy(wd => wd.DocumentId)
            .Where(g => (!allWords.Any() || allWords.All(word => g.Any(wd => wd.Word == word))) &&
                        (!anyWords.Any() || g.Any(wd => anyWords.Contains(wd.Word))) &&
                        (!excludeWords.Any() || !g.Any(wd => excludeWords.Contains(wd.Word))))
            .Select(g => g.Key);
        return await query.ToListAsync();
    }
}