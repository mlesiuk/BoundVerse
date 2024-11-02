using BoundVerse.Api.Extensions;
using BoundVerse.Api.Models;
using BoundVerse.Application;
using BoundVerse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = new Configuration();
builder.Configuration.Bind(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApi()
    .AddInfrastructure()
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
