using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;
using EcoSync.Modules.Sustainability.Application.Services;
using MediatR;

namespace EcoSync.Modules.Sustainability.Application.Sustainability.Queries.AnalyzeProduct;

public sealed class AnalyzeProductQueryHandler : IQueryHandler<AnalyzeProductQuery, SustainabilityAnalysisDto?>
{
    private readonly ISender _sender;
    private readonly ICarbonFootprintService _carbonFootprintService;

    public AnalyzeProductQueryHandler(
        ISender sender,
        ICarbonFootprintService carbonFootprintService)
    {
        _sender = sender;
        _carbonFootprintService = carbonFootprintService;
    }

    public async Task<SustainabilityAnalysisDto?> Handle(AnalyzeProductQuery request, CancellationToken cancellationToken)
    {
        // Get product information from Catalog module
        var getProductQuery = new GetProductQuery(request.ProductId);
        var product = await _sender.Send(getProductQuery, cancellationToken);

        if (product is null)
            return null;

        // Calculate carbon footprint score using AI
        var carbonScore = await _carbonFootprintService.CalculateCarbonFootprintScoreAsync(
            product.Material, 
            cancellationToken);

        // Determine sustainability rating based on score
        var rating = carbonScore switch
        {
            <= 20 => "Excellent",
            <= 40 => "Good",
            <= 60 => "Moderate",
            <= 80 => "Poor",
            _ => "Very Poor"
        };

        return new SustainabilityAnalysisDto(
            product.Id,
            product.Name,
            product.Material,
            carbonScore,
            rating,
            DateTime.UtcNow);
    }
}
