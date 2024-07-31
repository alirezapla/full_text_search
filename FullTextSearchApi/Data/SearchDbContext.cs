using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using FullTextSearchApi.Models;

namespace FullTextSearchApi.Data;

public class SearchDbContext : DbContext

{
    public SearchDbContext(DbContextOptions<SearchDbContext> options) : base(options)
    {
    }

    public DbSet<InvertedIndex> InvertedIndex { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Seed();
    }
}

public static class ModelBuilderExtensions
{
    private static readonly IConfiguration Configuration;

    public static void Seed(this ModelBuilder modelBuilder)
    {
        List<String> fileNames = new List<string>();
        var tokenizedDocuments = new List<String[]>();
        var uniqueTerms = new HashSet<String>();

        string[] files = Directory.GetFiles(Configuration["documentsPath"]);
        var documents = new List<String>();
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

        var rnd = new Random();
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