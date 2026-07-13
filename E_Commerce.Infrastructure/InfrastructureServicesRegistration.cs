using E_Commerce.Domain.Contarct;
using E_Commerce.Infrastructure.Date;
using E_Commerce.Infrastructure.DateSeeding;
using E_Commerce.Infrastructure.Repsitories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                );});
            services.AddKeyedScoped<IDateSeeder,CatlalogDataSeeder>("Catalog");
            services.AddScoped<IUniteOfWork, UniteOfWork>();
            return services;
        }
        
    }
}
