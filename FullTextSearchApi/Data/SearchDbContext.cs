using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using FullTextSearchApi.Models;

namespace FullTextSearchApi.Data;

public class SearchDbContext(DbContextOptions<SearchDbContext> options, IConfiguration configuration) : DbContext(options)

{
    private readonly IConfiguration _configuration = configuration;

    public DbSet<InvertedIndex> InvertedIndex { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Seed(_configuration["DocumentsPath"]);
    }
}

public static class ModelBuilderExtensions
{

    public static void Seed(this ModelBuilder modelBuilder,string? documentsPath)
    {
        List<string> fileNames = new List<string>();
        var tokenizedDocuments = new List<string[]>();
        var uniqueTerms = new HashSet<string>();

        string[] files = Directory.GetFiles(documentsPath ?? throw new InvalidOperationException());
        var documents = new List<string>();
        foreach (string file in files)
        {
            string content = File.ReadAllText(file);
            documents.Add(
                Regex.Replace(content, "[^a-zA-Z0-9_]+", " ", RegexOptions.Compiled).ToUpper());
            fileNames.Add(file.Split("\\").Last());
        }

        foreach (var document in documents)
        {
            tokenizedDocuments.Add(document.Split(" "));
        }

        foreach (var tokenizedDocument in tokenizedDocuments)
        {
            uniqueTerms.UnionWith(tokenizedDocument);
        }

        foreach (var uniqueTerm in uniqueTerms)
        {
            for (int docId = 1; docId < documents.Count; docId++)
            {
                if (documents[docId].Contains(uniqueTerm))
                {
                    modelBuilder.Entity<InvertedIndex>().HasData(new InvertedIndex
                        { Id = Guid.NewGuid().ToString(), DocumentId = fileNames[docId], Word = uniqueTerm });
                }
            }
        }
    }
}