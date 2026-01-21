using EcoSync.Modules.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcoSync.Modules.Catalog.Infrastructure.Database;

public static class DbInitializer
{
    public static async Task SeedAsync(CatalogDbContext context, ILogger logger)
    {
        try
        {
            // Check if data already exists
            if (await context.Products.AnyAsync())
            {
                logger.LogInformation("Database already contains products. Skipping seed.");
                return;
            }

            logger.LogInformation("Seeding database with sample products...");

            var products = new List<Product>
            {
                // Electronics
                Product.Create(
                    "Eco Solar Charger",
                    "Portable solar-powered charger for mobile devices with 20W output",
                    "Recycled Plastic & Solar Panels",
                    Money.Create(49.99m),
                    ProductCategory.Electronics,
                    50),

                Product.Create(
                    "Energy-Efficient LED Bulb Set",
                    "Pack of 4 smart LED bulbs with 90% energy savings",
                    "Recyclable Glass & LED",
                    Money.Create(29.99m),
                    ProductCategory.Electronics,
                    100),

                // Clothing
                Product.Create(
                    "Organic Cotton T-Shirt",
                    "100% organic cotton t-shirt, fair trade certified",
                    "Organic Cotton",
                    Money.Create(24.99m),
                    ProductCategory.Clothing,
                    75),

                Product.Create(
                    "Recycled Polyester Jacket",
                    "Water-resistant jacket made from recycled plastic bottles",
                    "Recycled Polyester",
                    Money.Create(89.99m),
                    ProductCategory.Clothing,
                    40),

                // Food
                Product.Create(
                    "Organic Quinoa",
                    "Premium organic quinoa, sustainably sourced from Peru",
                    "Organic Quinoa",
                    Money.Create(12.99m),
                    ProductCategory.Food,
                    200),

                // Books
                Product.Create(
                    "Sustainable Living Guide",
                    "Complete guide to eco-friendly living and zero waste lifestyle",
                    "Recycled Paper",
                    Money.Create(19.99m),
                    ProductCategory.Books,
                    60),

                // Sports
                Product.Create(
                    "Bamboo Yoga Mat",
                    "Natural bamboo fiber yoga mat, non-toxic and biodegradable",
                    "Bamboo Fiber",
                    Money.Create(44.99m),
                    ProductCategory.Sports,
                    35),

                Product.Create(
                    "Stainless Steel Water Bottle",
                    "Insulated water bottle keeps drinks cold for 24h, hot for 12h",
                    "Stainless Steel",
                    Money.Create(27.99m),
                    ProductCategory.Sports,
                    150),

                // EcoFriendly
                Product.Create(
                    "Reusable Produce Bags Set",
                    "Set of 5 mesh bags for groceries, zero waste shopping",
                    "Organic Cotton Mesh",
                    Money.Create(15.99m),
                    ProductCategory.EcoFriendly,
                    120),

                Product.Create(
                    "Compostable Phone Case",
                    "Fully biodegradable phone case, breaks down in 6 months",
                    "Plant-Based Bioplastic",
                    Money.Create(22.99m),
                    ProductCategory.EcoFriendly,
                    80)
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            logger.LogInformation("Successfully seeded {Count} products", products.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}
