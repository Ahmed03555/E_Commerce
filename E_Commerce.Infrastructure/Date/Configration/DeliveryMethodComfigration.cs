using E_Commerce.Domain.Entity.Orders;
using E_Commerce.Domain.Entity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Date.Configration
{
    internal class DeliveryMethodComfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(c => c.Price).HasColumnType("decimal (8,2)");
            builder.Property(D => D.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(D => D.ShortName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(D => D.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);


        }
    }
}
