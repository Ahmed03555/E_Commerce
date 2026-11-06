using E_Commerce.Domain.Contarct;

namespace E_Commerce.Api
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> SeedAndMigrationAsync(this WebApplication application) 
        {
            using var scope = application.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDateSeeder>("Catalog");
            var identitySeedere = scope.ServiceProvider.GetRequiredKeyedService<IDateSeeder>("Identity");


            await seeder.SeedDataAsync();
            await identitySeedere.SeedDataAsync();
            return application;
        }
    }
}
