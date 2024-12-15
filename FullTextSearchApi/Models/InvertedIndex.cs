using System.ComponentModel.DataAnnotations;

namespace FullTextSearchApi.Models;

public class InvertedIndex
{
    public required string Id { get; init; }
    [MaxLength(64)] public required string Word { get; init; }
    [MaxLength(16)] public required string DocumentId { get; init; }
}