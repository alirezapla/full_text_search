using FullTextSearchApi.Data;
using FullTextSearchApi.DataAccessLayer;
using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.Repositories;

public class InvertedIndexRepository(SearchDbContext context)
    : BaseRepository<InvertedIndex>(context), IInvertedIndexRepository
{
    public async Task<List<string>> Get(string query)
    {
        var data = await FindAsync(index => index.Word == query.ToUpper());
        return data.Select(x=>x.DocumentId).ToList();
    }

    public async Task<List<string>> ConditionalGet(
        HashSet<string> allWords, HashSet<string> anyWords, HashSet<string> excludeWords)
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