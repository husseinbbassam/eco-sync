namespace EcoSync.Modules.Sustainability.Application.Services;

public interface ICarbonFootprintService
{
    Task<decimal> CalculateCarbonFootprintScoreAsync(string materialDescription, CancellationToken cancellationToken = default);
}
