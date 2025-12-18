/// <summary>
/// Entry point for the FirstAPI application.
/// Configures services, middleware, and starts the web application.
/// </summary>
using FirstAPI.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Build connection string from environment variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? $"Server={Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost"};" +
       $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"};" +
       $"Database={Environment.GetEnvironmentVariable("DB_NAME") ?? "firstapi_data"};" +
       $"User={Environment.GetEnvironmentVariable("DB_USER") ?? "root"};" +
       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

builder.Services.AddDbContext<FirstAPIContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 21))
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
