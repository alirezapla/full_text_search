using System.Net;
using FullTextSearchApi.Services.Abstractions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FullTextSearchApiTests;

public class FulltextSearchControllerTests : IAsyncLifetime
{
    private readonly Mock<IInvertedIndexService> _invertedIndexServiceMock = new();
    private HttpClient _httpClient = null!;

    public async Task InitializeAsync()
    {
        var hostBuilder = Host.CreateDefaultBuilder(new string[0])
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder.UseTestServer(); 
                webHostBuilder.ConfigureServices(services =>
                {
                    services.AddSingleton(_invertedIndexServiceMock.Object);
                    services.AddControllers();  
                });
                webHostBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UsePathBase(new PathString("/api/v1"));

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();  
                    });
                });
            })
            
            .ConfigureServices((_, services) => { services.AddSingleton(_invertedIndexServiceMock.Object); });
        
        var host = await hostBuilder.StartAsync();
        _httpClient = host.GetTestClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5158");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }


    [Fact]
    public async Task GetInvertedIndexAsync_ShouldReturnListOfIds_WhenCallEndpointWithQuery()
    {
        var query = "them";
        var result = new List<string> { "58043", "58044", "58045" };
        _invertedIndexServiceMock.Setup(
            invertedIndexService => invertedIndexService.SearchAsync(query)).ReturnsAsync(result);
        var response = await _httpClient.GetAsync($"/api/v1/Search?query={query}");
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var returnedJson = await response.Content.ReadAsStringAsync();
        Console.WriteLine(returnedJson);
    }
}