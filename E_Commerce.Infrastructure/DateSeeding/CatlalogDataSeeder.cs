using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Orders;
using E_Commerce.Domain.Entity.Products;
using E_Commerce.Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Infrastructure.DateSeeding
{
    internal class CatlalogDataSeeder : IDateSeeder
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<CatlalogDataSeeder> _logger;

        public CatlalogDataSeeder(StoreDbContext context, ILogger<CatlalogDataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var PendingMigrations = await _context.Database.GetPendingMigrationsAsync(cancellationToken);
                if (PendingMigrations.Any())
                {
                    await _context.Database.GetAppliedMigrationsAsync(cancellationToken);
                }
                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DateSeeding", "SeedData");
                await SeedIfEmpty<Product, int>(seedRoot, "products.json", cancellationToken);
                await SeedIfEmpty<ProductBrand, int>(seedRoot, "brands.json", cancellationToken);
                await SeedIfEmpty<ProductType, int>(seedRoot, "types.json", cancellationToken);
                await SeedIfEmpty<DeliveryMethod, int>(seedRoot, "delivery.json", cancellationToken);


                var Result = await _context.SaveChangesAsync(cancellationToken);
                if (Result > 0)
                {
                    _logger.LogInformation("Data seeding completed successfully.");
                }
                else
                {
                    _logger.LogInformation("No new data was added during seeding.");
                }

            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                
            }
        }


        private async Task SeedIfEmpty<T, TKey>(string rootPath, string fileName, CancellationToken cancellationToken) where T : BaseEntity<TKey>
        {
            if(await _context.Set<T>().AnyAsync(cancellationToken))
            {
                _logger.LogInformation($"The {typeof(T).Name} table already has data. Skipping seeding.");
                return;
            }
            var filePath = Path.Combine(rootPath, fileName);

            if(!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found: {filePath}");
                return;
            }
            using var fileStream = File.OpenRead(filePath);
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, option, cancellationToken);
            if(items?.Any()?? false)
                _context.Set<T>().AddRange(items);
        }

    }
}
