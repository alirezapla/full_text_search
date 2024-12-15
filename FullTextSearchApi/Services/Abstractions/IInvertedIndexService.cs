namespace FullTextSearchApi.Services.Abstractions;

public interface IInvertedIndexService
{
    Task<List<string>> SearchAsync(string query);

}