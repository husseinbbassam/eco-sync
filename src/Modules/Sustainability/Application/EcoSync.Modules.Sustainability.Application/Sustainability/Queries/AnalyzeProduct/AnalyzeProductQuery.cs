using EcoSync.BuildingBlocks.Application;

namespace EcoSync.Modules.Sustainability.Application.Sustainability.Queries.AnalyzeProduct;

public sealed record AnalyzeProductQuery(Guid ProductId) : IQuery<SustainabilityAnalysisDto?>;
