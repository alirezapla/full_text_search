<<<<<<< HEAD
﻿using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using FullTextSearchApi.UnitOfWorks;
=======
﻿using FullTextSearchApi.Data;
using FullTextSearchApi.DataAccessLayer;
using FullTextSearchApi.Models;
using FullTextSearchApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchApi.Repositories;

<<<<<<< HEAD
public class InvertedIndexRepository : IInvertedIndexRepository
{
    private readonly DbSet<InvertedIndex> _dbSet;

    private readonly IUnitOfwork _unitOfWork;

    public InvertedIndexRepository(IUnitOfwork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbSet = _unitOfWork.Context.Set<InvertedIndex>();
    }

    public async Task<InvertedIndex> Create(InvertedIndex invertedIndex)
    {
        _dbSet.Add(invertedIndex);
        await _unitOfWork.SaveChangesAsync();
        return invertedIndex;
    }

    public async Task<List<string>> Get(string query)
    {
        var data = await _dbSet.Where(x => x.Word.Equals(query, StringComparison.CurrentCultureIgnoreCase))
            .Select(x => x.DocumentId).ToListAsync();
        return data;
    }

    public async Task<List<string>> ConditionalGet(
        HashSet<string> allWords, HashSet<string> anyWords, HashSet<string> excludeWords)
    {
        var query = _dbSet
            .Where(wd => allWords.Contains(wd.Word) || anyWords.Contains(wd.Word) || excludeWords.Contains(wd.Word))
            .GroupBy(wd => wd.DocumentId)
            .Where(g => (allWords.Count == 0 || allWords.All(word => g.Any(wd => wd.Word == word))) &&
                        (anyWords.Count == 0 || g.Any(wd => anyWords.Contains(wd.Word))) &&
                        (excludeWords.Count == 0 || !g.Any(wd => excludeWords.Contains(wd.Word))))
            .Select(g => g.Key);
        return await query.ToListAsync();
    }

    public async void Delete(int id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data == null) return;
        _dbSet.Remove(data);
        await _unitOfWork.SaveChangesAsync();
    }
=======
public class InvertedIndexRepository : BaseRepository<InvertedIndex>, IInvertedIndexRepository
{
    public InvertedIndexRepository(SearchDbContext context) : base(context)
    {
    }

    public async Task<List<String>> Get(String query)
    {
        var data = await Context.InvertedIndex.Where(x => x.Word == query.ToUpper())
            .Select(x => x.DocumentId)
            .ToListAsync();
        return data;
    }

    public async Task<List<String>> ConditionalGet(
        HashSet<String> allWords, HashSet<String> anyWords, HashSet<String> excludeWords)
    {
        var query = Context.InvertedIndex
            .Where(wd => allWords.Contains(wd.Word) || anyWords.Contains(wd.Word) || excludeWords.Contains(wd.Word))
            .GroupBy(wd => wd.DocumentId)
            .Where(g => (!allWords.Any() || allWords.All(word => g.Any(wd => wd.Word == word))) &&
                        (!anyWords.Any() || g.Any(wd => anyWords.Contains(wd.Word))) &&
                        (!excludeWords.Any() || !g.Any(wd => excludeWords.Contains(wd.Word))))
            .Select(g => g.Key);
        return await query.ToListAsync();
    }
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
}