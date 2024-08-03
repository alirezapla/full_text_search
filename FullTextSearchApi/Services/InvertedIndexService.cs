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
<<<<<<< HEAD

    public async Task<List<string>> SearchAsync(string query)
=======
    
    public async Task<List<String>> Search(string query)
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
    {
        if (query.Split(" ").Length == 1 && MyRegex().IsMatch(query))
        {
            return await _invertedIndexRepository.Get(query);
        }

        return await _invertedIndexRepository.ConditionalGet(
            FilterUnSignedWords(query), FilterOrSignedWords(query), FilterNotSignedWords(query));
    }
<<<<<<< HEAD

    private static HashSet<string> FilterUnSignedWords(string words)
=======
    
    private static HashSet<String> FilterUnSignedWords(String words)
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
    {
        return words.Split(" ").Where(word => MyRegex1().IsMatch(word)).ToHashSet();
    }

<<<<<<< HEAD
    private static HashSet<string> FilterOrSignedWords(string words)
=======
    private static HashSet<String> FilterOrSignedWords(String words)
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
    {
        return words.Split(" ").Where(word => word[0] == '+').Select(word => word.Substring(1)).ToHashSet();
    }

<<<<<<< HEAD
    private static HashSet<string> FilterNotSignedWords(string words)
=======
    private static HashSet<String> FilterNotSignedWords(String words)
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
    {
        return words.Split(" ").Where(word => word[0] == '-').Select(word => word.Substring(1)).ToHashSet();
    }

    [GeneratedRegex(@"^[aA-zZ]")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"^[aA-zZ]")]
    private static partial Regex MyRegex1();
}