using Hospital.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class StorageUnitConfiguration : IEntityTypeConfiguration<StorageUnit>
{
       public void Configure(EntityTypeBuilder<StorageUnit> builder)
       {
              builder.HasKey(s => s.Id);

              builder.Property(l => l.Name)
                     .IsRequired()
                     .HasMaxLength(200);

              builder.Property(l => l.Description)
                     .IsRequired()
                     .HasMaxLength(500);

              builder.Property(l => l.Address)
                     .IsRequired()
                     .HasMaxLength(300);

              builder.HasOne(s => s.Department)
                     .WithMany(d => d.StorageUnits)
                     .HasForeignKey(s => s.DepartmentId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}