using FullTextSearchApi.Models;
using Microsoft.AspNetCore.Mvc;
using FullTextSearchApi.Models.DTO;
using FullTextSearchApi.Services;


namespace FullTextSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly IInvertedIndexService _service;

    public SearchController(IInvertedIndexService service, ILogger<SearchController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetInvertedIndexAsync([FromQuery] string query)
    {
        _logger.LogInformation($"request received with `{query}` in method GetInvertedIndex");
        var textIds = await _service.SearchAsync(query.ToUpper());
        _logger.LogDebug($"Get method called, got {textIds} results");
        return Ok(new ApiResponse<List<string>>
        {
            StatusCode = 200,
            Message = "Document ids",
            Data = textIds
        });
    }
}