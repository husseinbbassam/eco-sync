using EcoSync.Modules.Catalog.API;
using EcoSync.Modules.Sustainability.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add modules
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddSustainabilityModule(builder.Configuration);

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

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

// Map module endpoints
app.MapCatalogEndpoints();
app.MapSustainabilityEndpoints();

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

