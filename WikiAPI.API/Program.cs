using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WikiAPI.Application;
using WikiAPI.Persistence;
using Microsoft.OpenApi.Models;
using WikiAPI.Infrastructure;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Contracts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Wiki API"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WikiAPI V1");
});

app.UseEndpoints(endpoints =>
{
    app.MapControllers();
});

// Seeding Database
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var wikiApiDbContext = scopedProvider.GetRequiredService<IApplicationDbContext>();
        var articleRepository = scopedProvider.GetRequiredService<IArticleRepository>();
        var sourceRepository = scopedProvider.GetRequiredService<ISourceRepository>();
        var jsonImporter = scopedProvider.GetRequiredService<IJsonImporter>();
        await WikiAPIDbContextSeed.SeedAsync(wikiApiDbContext, jsonImporter, articleRepository, sourceRepository);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred seeding the DB. Exception: {ex.Message}");
    }
}

app.Run();
