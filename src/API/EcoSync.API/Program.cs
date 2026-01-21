using EcoSync.Modules.Catalog.API;
using EcoSync.Modules.Sustainability.API;
using EcoSync.Modules.Catalog.Infrastructure.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add modules
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddSustainabilityModule(builder.Configuration);

// Add Health Checks
// Note: Add health checks for other module DbContexts as they are registered in the application
builder.Services.AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString("Database")!,
        name: "database",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "sql", "postgres" })
    .AddDbContextCheck<CatalogDbContext>(
        name: "catalog-db-context",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "catalog" });

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "EcoSync API",
        Version = "v1",
        Description = "Modular Monolith API for EcoSync application"
    });
});

var app = builder.Build();

// Run database migrations on startup
try
{
    using var scope = app.Services.CreateScope();
    
    // Migrate Catalog database
    // Note: Add other module DbContexts here as they are registered in the application
    var catalogDbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<Program>>();
    
    Log.Information("Running database migrations...");
    await catalogDbContext.Database.MigrateAsync();
    Log.Information("Database migrations completed successfully");
    
    // Seed database with sample data
    await EcoSync.Modules.Catalog.Infrastructure.Database.DbInitializer.SeedAsync(catalogDbContext, logger);
}
catch (Exception ex)
{
    Log.Error(ex, "An error occurred while migrating the database");
    throw;
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

// Map health check endpoints
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                duration = e.Value.Duration.TotalMilliseconds
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds
        });
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("db")
});

app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false // No checks, just returns if the app is running
});

// Map module endpoints
app.MapCatalogEndpoints();
app.MapSustainabilityModuleEndpoints();

app.MapGet("/", () => "EcoSync API is running!")
    .WithName("Health")
    .WithTags("Health");

try
{
    Log.Information("Starting EcoSync API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

