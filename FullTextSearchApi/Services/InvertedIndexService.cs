using System.Text.RegularExpressions;
using FullTextSearchApi.Data.Repositories;
using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Services;

public class InvertedIndexService : IInvertedIndexService
{
    private readonly IInvertedIndexRepository _invertedIndexRepository;
    private Dictionary<String, HashSet<String>> _invertedIndex;


    public InvertedIndexService(IInvertedIndexRepository invertedIndexRepository)
    {
        _invertedIndexRepository =
            invertedIndexRepository ?? throw new ArgumentNullException(nameof(invertedIndexRepository));
        _invertedIndex = new Dictionary<string, HashSet<String>>();
    }


    public async Task<List<String>> Search(string query)
    {
        if (query.Split(" ").Count() == 1 && Regex.IsMatch(query, @"^[aA-zZ]"))
        {
            return await _invertedIndexRepository.Get(query);
        }

        return await _invertedIndexRepository.ConditionalGet(
            FilterUnSignedWords(query), FilterOrSignedWords(query), FilterNotSignedWords(query));
    }


    private HashSet<String> FilterUnSignedWords(String words)
    {
        return words.Split(" ").Where(word => Regex.IsMatch(word, @"^[aA-zZ]")).ToHashSet();
    }

    private HashSet<String> FilterOrSignedWords(String words)
    {
        return words.Split(" ").Where(word => word[0] == '+').Select(word => word.Substring(1)).ToHashSet();
    }

    private HashSet<String> FilterNotSignedWords(String words)
    {
        return words.Split(" ").Where(word => word[0] == '-').Select(word => word.Substring(1)).ToHashSet();
    }
}