using FullTextSearchApi.DataAccessLayer.Abstractions;
using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Repositories.Abstractions;

public interface IInvertedIndexRepository : IBaseRepository<InvertedIndex>
{
    public Task<List<string>> Get(string query);

    public Task<List<string>> ConditionalGet(HashSet<string> filterWords1, HashSet<string> filterWords2,
        HashSet<string> excludeWords);
}