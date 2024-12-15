using System.Text.RegularExpressions;
using FullTextSearchApi.Repositories.Abstractions;
using FullTextSearchApi.Services.Abstractions;

namespace FullTextSearchApi.Services;

public partial class InvertedIndexService(IInvertedIndexRepository invertedIndexRepository) : IInvertedIndexService
{
    private readonly IInvertedIndexRepository _invertedIndexRepository = invertedIndexRepository ?? throw new ArgumentNullException(nameof(invertedIndexRepository));

    public async Task<List<string>> SearchAsync(string query)
    {
        if (query.Split(" ").Length == 1 && MyRegex().IsMatch(query))
        {
            return await _invertedIndexRepository.GetAsync(query);
        }

        return await _invertedIndexRepository.ConditionalGetAsync(
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