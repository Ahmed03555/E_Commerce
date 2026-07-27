using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contarct;
using E_Commerce.Infrastructure.Date;
using E_Commerce.Infrastructure.DateSeeding;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using E_Commerce.Infrastructure.Identity.Services;
using E_Commerce.Infrastructure.Repsitories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection")
                );
            });
            services.AddKeyedScoped<IDateSeeder,CatlalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDateSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUniteOfWork, UniteOfWork>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddSingleton<IConnectionMultiplexer>(Config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.AddScoped<IIdentityServices, IdentityServices>();
            return services;
        }
        
    }
}
