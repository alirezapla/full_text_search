using FullTextSearchApi.Data;
using FullTextSearchApi.Data.Repositories;
using FullTextSearchApi.Services;
using FullTextSearchApi.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File("logs/serilogFile.txt",
        outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


builder.Services.AddDbContext<SearchDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IInvertedIndexRepository, InvertedIndexRepository>();
builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped<IInvertedIndexService, InvertedIndexService>();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();