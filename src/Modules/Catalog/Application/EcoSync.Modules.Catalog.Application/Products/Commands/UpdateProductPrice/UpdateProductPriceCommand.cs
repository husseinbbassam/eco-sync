using EcoSync.BuildingBlocks.Application;

namespace EcoSync.Modules.Catalog.Application.Products.Commands.UpdateProductPrice;

public sealed record UpdateProductPriceCommand(Guid ProductId, decimal NewPrice, string Currency) : ICommand;
