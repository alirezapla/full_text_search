using FullTextSearchApi.Data;
using FullTextSearchApi.DataAccessLayer;
using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.Repositories;

public class InvertedIndexRepository(SearchDbContext context)
    : BaseRepository<InvertedIndex>(context), IInvertedIndexRepository
{
    public async Task<List<String>> Get(String query)
    {
        // var data = await FindAsync(index => index.Word == query.ToUpper());
        var data = await Context.InvertedIndex.Where(x => x.Word == query.ToUpper())
            .Select(x => x.DocumentId)
            .ToListAsync();
        // return data.Select(x=>x.DocumentId).ToList();
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