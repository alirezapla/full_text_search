using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Services;

public interface IInvertedIndexService
{
    Task<List<string>> SearchAsync(string query);

}