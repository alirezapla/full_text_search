using FullTextSearchApi.Models;

namespace FullTextSearchApi.Repositories.Abstractions;

public interface IInvertedIndexRepository
{
    public Task<List<string>> Get(string query);

    public Task<List<string>> ConditionalGet(HashSet<string> filterWords1, HashSet<string> filterWords2,
        HashSet<string> excludeWords);

    public Task<InvertedIndex> Create(InvertedIndex invertedIndex);
    public void Delete(int id);
}