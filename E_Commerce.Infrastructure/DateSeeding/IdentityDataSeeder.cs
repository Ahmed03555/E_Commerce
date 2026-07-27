using E_Commerce.Domain.Contarct;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DateSeeding
{
    internal class IdentityDataSeeder : IDateSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<StoreIdentityDbContext> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext ,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager ,ILogger<StoreIdentityDbContext> logger)
        {
            _dbContext=dbContext;
            _userManager=userManager;
            _roleManager=roleManager;
            _logger=logger;
        }
        public async Task SeedDataAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
                if (!pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);

                if (!await _roleManager.Roles.AnyAsync(cancellationToken))
                {
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!await _userManager.Users.AnyAsync(cancellationToken))
                {
                    var superAdmin = new ApplicationUser()
                    {
                        DisplayName = "Ahmed Yasser",
                        Email = "ahmed@gmail.com",
                        PhoneNumber ="01026634877",
                        UserName = "Ahmed"
                    };
                    var result = await _userManager.CreateAsync(superAdmin, "P@ssw0rd");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                    }
                    else
                    {
                        _logger.LogWarning($"Cannot Seed Default Super Admin User: {string.Join(", ", result.Errors.Select(E => E.Description))}");
                    }
                }
            }
            catch (Exception ex)
            {


                _logger.LogError(ex, "Identity Data Seeding Failed");
            }

        }
    }
}
