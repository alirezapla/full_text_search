using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Data.Repositories;

public interface IInvertedIndexRepository
{
    public Task<List<String>> Get(String query);
    public Task<List<String>> ConditionalGet(HashSet<String> filterWords1, HashSet<String> filterWords2, HashSet<String> excludeWords);
    public Task<ActionResult<InvertedIndex>> Create(InvertedIndex invertedIndex);
    public void Delete(int id);
}