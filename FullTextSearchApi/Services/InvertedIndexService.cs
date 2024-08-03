using System.Text.RegularExpressions;
using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Services;

public partial class InvertedIndexService : IInvertedIndexService
{
    private readonly IInvertedIndexRepository _invertedIndexRepository;

    public InvertedIndexService(IInvertedIndexRepository invertedIndexRepository)
    {
        _invertedIndexRepository =
            invertedIndexRepository ?? throw new ArgumentNullException(nameof(invertedIndexRepository));
    }

    public async Task<List<string>> SearchAsync(string query)
    {
        if (query.Split(" ").Length == 1 && MyRegex().IsMatch(query))
        {
            return await _invertedIndexRepository.Get(query);
        }

        return await _invertedIndexRepository.ConditionalGet(
            FilterUnSignedWords(query), FilterOrSignedWords(query), FilterNotSignedWords(query));
    }

    private static HashSet<string> FilterUnSignedWords(string words)
    {
        return words.Split(" ").Where(word => MyRegex1().IsMatch(word)).ToHashSet();
    }

    private static HashSet<string> FilterOrSignedWords(string words)
    {
        return words.Split(" ").Where(word => word[0] == '+').Select(word => word.Substring(1)).ToHashSet();
    }

    private static HashSet<string> FilterNotSignedWords(string words)
    {
        return words.Split(" ").Where(word => word[0] == '-').Select(word => word.Substring(1)).ToHashSet();
    }

    [GeneratedRegex(@"^[aA-zZ]")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"^[aA-zZ]")]
    private static partial Regex MyRegex1();
}