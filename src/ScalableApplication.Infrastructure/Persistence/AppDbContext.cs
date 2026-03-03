using Microsoft.EntityFrameworkCore;
using ScalableApplication.Domain.Configurations;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext (options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreCategory> StoreCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new RetailerConfiguration());
            modelBuilder.ApplyConfiguration(new StoreCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
        }
    }
}
