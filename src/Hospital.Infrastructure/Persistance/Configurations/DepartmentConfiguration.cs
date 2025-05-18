using Hospital.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
       public void Configure(EntityTypeBuilder<Department> builder)
       {
              builder.HasKey(d => d.Id);

              builder.Property(l => l.Name)
                     .IsRequired()
                     .HasMaxLength(200);

              builder.Property(l => l.Description)
                     .IsRequired()
                     .HasMaxLength(500);

              builder.Property(l => l.Address)
                     .IsRequired()
                     .HasMaxLength(300);

              builder.HasMany(d => d.Rooms)
                     .WithOne(r => r.Department)
                     .HasForeignKey(r => r.DepartmentId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasMany(d => d.StorageUnits)
                     .WithOne(s => s.Department)
                     .HasForeignKey(s => s.DepartmentId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}