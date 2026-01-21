namespace EcoSync.Modules.Sustainability.Application.Sustainability.Queries.AnalyzeProduct;

public sealed record SustainabilityAnalysisDto(
    Guid ProductId,
    string ProductName,
    string Material,
    decimal CarbonFootprintScore,
    string SustainabilityRating,
    DateTime AnalyzedAt);
