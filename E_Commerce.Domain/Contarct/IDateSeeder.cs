using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contarct
{
    public interface IDateSeeder
    {
        Task SeedDataAsync(CancellationToken cancellationToken = default);
    }
}
