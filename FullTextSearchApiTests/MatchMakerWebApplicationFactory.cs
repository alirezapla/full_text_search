using FullTextSearchApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FullTextSearchApiTests;

internal class MatchMakerWebApplicationFactory : WebApplicationFactory<Program>
{
    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<SearchDbContext>));

            services.AddDbContext<SearchDbContext>(options =>
                options.UseNpgsql(GetConnectionString()));
            SearchDbContext dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
        });
    }

    private static string? GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<MatchMakerWebApplicationFactory>()
            .Build();

        var connString = configuration.GetConnectionString("DbConnection");
        return connString;
    }

    private static SearchDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SearchDbContext>();
        return dbContext;
    }
}