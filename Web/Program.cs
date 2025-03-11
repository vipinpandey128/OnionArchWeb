using Domain.Repositories;
using Persistence.Repositories;
using Persistence;
using Services.Abstractions;
using Services;
using Microsoft.EntityFrameworkCore;
using Web.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

// Securely override password
var baseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbPassword))
{
    Console.WriteLine("Critical: DB_PASSWORD environment variable is missing.");
    throw new InvalidOperationException("Database password is not set in environment variables (DB_PASSWORD)");
}

var finalConnectionString = $"{baseConnectionString}Password={dbPassword};";


builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(finalConnectionString));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();


var app = builder.Build();

// Apply Migrations
await ApplyMigrations(app.Services);

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Secure DB Connection Initiaization");

await app.RunAsync();


// Method to apply migrations
static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    await dbContext.Database.MigrateAsync();
}


