using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Domain.Configurations
{
    public class RetailerConfiguration : IEntityTypeConfiguration<Retailer>
    {
        public void Configure(EntityTypeBuilder<Retailer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Code, x.DisabledDate }).IsUnique();
        }
    }

    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Code, x.RetailerCode, x.DisabledDate }).IsUnique();
            builder.HasOne(s => s.Retailer)
                .WithMany(r => r.Stores)
                .HasForeignKey(s => s.RetailerCode)
                .HasPrincipalKey(r => r.Code);
            builder.HasOne(s => s.Category)
                .WithMany(sc => sc.Stores)
                .HasForeignKey(s => s.StoreCategoryId)
                .HasPrincipalKey(sc => sc.Id);
        }
    }

    public class StoreCategoryConfiguration : IEntityTypeConfiguration<StoreCategory>
    {
        public void Configure(EntityTypeBuilder<StoreCategory> builder)
        {
            builder.HasKey(x => x.Id);
            int id = 1;
            builder.HasData(
                new StoreCategory
                {
                    Id = id++,
                    Name = "Department",
                    Description = "Department stores aim to provide shoppers with a comprehensive shopping experience all under one roof, including clothing, accessories, home appliances, and electronics.",
                    LiveDate = new DateTime(2026,03,01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Specialty",
                    Description = "Retailers that focus on a specific, narrow product category or niche, offering deep assortment and expert knowledge.",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Convenience",
                    Description = "Small, conveniently located stores (often in residential areas) operating long hours with a limited, high-turnover selection of essentials.",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Discount Retailers",
                    Description = "Stores that sell a broad assortment of products (both hard and soft goods) at low prices (e.g., Ross Dress for Lessthis PDF notes that Ross Dress for Less and Grocery Outlet are discount retailers).",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Warehouse Clubs",
                    Description = "Large, warehouse-style stores selling products in bulk at discounted prices to members (e.g., Costco).",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Outlet Stores",
                    Description = "Retailers that sell goods, often from previous seasons or surplus stock, at reduced prices.",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                },
                new StoreCategory
                {
                    Id = id++,
                    Name = "Itinerant / Mobile Retailers",
                    Description = " Non-fixed sellers who move from place to place, such as hawkers, peddlers, and street vendors.",
                    LiveDate = new DateTime(2026, 03, 01),
                    DisabledDate = null
                }
            );
        }
    }
}
