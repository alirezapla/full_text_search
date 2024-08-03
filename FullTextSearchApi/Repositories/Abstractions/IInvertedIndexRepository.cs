<<<<<<< HEAD
﻿using FullTextSearchApi.Models;

namespace FullTextSearchApi.Repositories.Abstractions;

public interface IInvertedIndexRepository
=======
﻿using FullTextSearchApi.DataAccessLayer.Abstractions;
using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchApi.Repositories.Abstractions;

public interface IInvertedIndexRepository : IBaseRepository<InvertedIndex>
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
{
    public Task<List<string>> Get(string query);

    public Task<List<string>> ConditionalGet(HashSet<string> filterWords1, HashSet<string> filterWords2,
        HashSet<string> excludeWords);
<<<<<<< HEAD

    public Task<InvertedIndex> Create(InvertedIndex invertedIndex);
    public void Delete(int id);
=======
>>>>>>> 0a4227ff90beb95245c56902a1da342eeb52e8f0
}