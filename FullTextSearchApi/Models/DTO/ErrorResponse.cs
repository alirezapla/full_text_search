namespace FullTextSearchApi.Models.DTO;

public sealed class ErrorResponse
{
    public string? Message { get; set; }
    public string? Title { get; set; }
    public int StatusCode { get; set; }
}