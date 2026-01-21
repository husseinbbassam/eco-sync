using EcoSync.Modules.Sustainability.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace EcoSync.Modules.Sustainability.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSustainabilityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var apiKey = configuration["AI:ApiKey"] 
            ?? Environment.GetEnvironmentVariable("AI_API_KEY") 
            ?? throw new InvalidOperationException("AI API Key is not configured. Set AI:ApiKey in configuration or AI_API_KEY environment variable.");

        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            modelId: "gpt-4o-mini",
            apiKey: apiKey);

        var kernel = builder.Build();
        
        services.AddSingleton(kernel);
        services.AddScoped<ICarbonFootprintService, CarbonFootprintService>();

        return services;
    }
}
