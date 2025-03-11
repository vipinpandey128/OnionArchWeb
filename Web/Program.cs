using Domain.Repositories;
using Persistence.Repositories;
using Persistence;
using Services.Abstractions;
using Services;
using Microsoft.EntityFrameworkCore;
using Web.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddSwaggerGen(c =>
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

await app.RunAsync();


// Method to apply migrations
static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    await dbContext.Database.MigrateAsync();
}


