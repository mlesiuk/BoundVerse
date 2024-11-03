using BoundVerse.Api.Extensions;
using BoundVerse.Api.Models;
using BoundVerse.Application;
using BoundVerse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection("Configuration");
builder.Services
    .AddOptions<Configuration>()
    .Bind(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseConnectionString = configuration
    .GetSection("Database")?
    .GetSection("ConnectionString")?
    .Value;

builder.Services
    .AddApi()
    .AddInfrastructure(databaseConnectionString)
    .AddApplication();

builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();
