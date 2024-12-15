namespace FullTextSearchApi.Models.DTO;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public required T Data { get; set; }
}