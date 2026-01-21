using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace EcoSync.Modules.Sustainability.Application.Services;

public sealed class CarbonFootprintService : ICarbonFootprintService
{
    private readonly Kernel _kernel;

    public CarbonFootprintService(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<decimal> CalculateCarbonFootprintScoreAsync(string materialDescription, CancellationToken cancellationToken = default)
    {
        var chatService = _kernel.GetRequiredService<IChatCompletionService>();
        
        var chatHistory = new ChatHistory();
        chatHistory.AddSystemMessage(@"You are an environmental expert that calculates carbon footprint scores for product materials.
Based on the material description provided, calculate a carbon footprint score from 0 to 100, where:
- 0-20: Excellent (highly sustainable materials like bamboo, recycled materials)
- 21-40: Good (renewable materials with low environmental impact)
- 41-60: Moderate (conventional materials with some sustainability concerns)
- 61-80: Poor (materials with high environmental impact)
- 81-100: Very Poor (highly polluting or non-renewable materials)

IMPORTANT: You must respond with ONLY a number between 0 and 100. Do not include any explanatory text, just the numeric score.");

        chatHistory.AddUserMessage($"Calculate the carbon footprint score for this material: {materialDescription}");

        var result = await chatService.GetChatMessageContentAsync(chatHistory, cancellationToken: cancellationToken);
        
        var scoreText = result.Content?.Trim() ?? "50";
        
        // Try to parse the score, defaulting to 50 (moderate) if parsing fails
        if (decimal.TryParse(scoreText, out var score))
        {
            // Ensure the score is within the valid range
            return Math.Clamp(score, 0, 100);
        }
        
        return 50; // Default to moderate if parsing fails
    }
}
