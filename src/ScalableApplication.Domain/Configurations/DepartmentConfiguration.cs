using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Domain.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasIndex(d => d.Name).IsUnique();

            builder.HasData(
                new Department
                {
                    Id = new Guid("1A4AB0C3-24FE-4C39-AB21-72D38AB3F413"),
                    Name = "Sales & Marketing",
                    Description = "Sales & Marketing",
                    IsActive = true,
                    CreatedTime = new DateTime(2026,02,25)
                },
                new Department
                {
                    Id = new Guid("D8FC5BF3-1211-4567-A234-513DD8FFF314"),
                    Name = "IT",
                    Description = "Infrastructure",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                },
                new Department
                {
                    Id = new Guid("87355B23-6C56-434E-A59B-BDAA623226E3"),
                    Name = "Engineering",
                    Description = "Engineering",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                },
                new Department
                {
                    Id = new Guid("5F1D6C8E-3E2A-4F7B-9C1D-8A2F4E5B6C7D"),
                    Name = "Human Resources",
                    Description = "Human Resources",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                },
                new Department
                {
                    Id = new Guid("9B2E7F4A-8C3D-4E5F-9A1B-2C3D4E5F6A7B"),
                    Name = "Finance",
                    Description = "Finance",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                },
                new Department
                {
                    Id = new Guid("C4D5E6F7-8A9B-0C1D-2E3F-4A5B6C7D8E9F"),
                    Name = "Customer Support",
                    Description = "Customer Support",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                },
                new Department
                {
                    Id = new Guid("2FB0DDDA-716A-4827-82CA-01026A34FC3E"),
                    Name = "Bench",
                    Description = "Not Billable",
                    IsActive = true,
                    CreatedTime = new DateTime(2026, 02, 25)
                }
            );
        }
    }
}
