using FullTextSearchApi.DataAccessLayer.Abstractions;
using FullTextSearchApi.Models;

namespace FullTextSearchApi.Repositories.Abstractions;

public interface IInvertedIndexRepository : IBaseRepository<InvertedIndex>
{
    public Task<List<string>> GetAsync(string query);

    public Task<List<string>> ConditionalGetAsync(HashSet<string> filterWords1, HashSet<string> filterWords2,
        HashSet<string> excludeWords);
}