using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class HospitalConfiguration : IEntityTypeConfiguration<Domain.Entities.Locations.Hospital>
{
       public void Configure(EntityTypeBuilder<Domain.Entities.Locations.Hospital> builder)
       {
              builder.HasKey(h => h.Id);

              builder.Property(h => h.Name)
                     .IsRequired()
                     .HasMaxLength(200);

              builder.Property(h => h.Address)
                     .IsRequired()
                     .HasMaxLength(300);

              builder.HasMany(h => h.GeneralPractitioners)
                     .WithOne(d => d.Hospital)
                     .HasForeignKey(d => d.HospitalId)
                     .OnDelete(DeleteBehavior.SetNull);

              builder.HasMany(h => h.Nurses)
                     .WithOne(n => n.Hospital)
                     .HasForeignKey(n => n.HospitalId)
                     .OnDelete(DeleteBehavior.SetNull);

              builder.HasMany(h => h.MedicalDevices)
                     .WithOne(m => m.Hospital)
                     .HasForeignKey(m => m.HospitalId)
                     .OnDelete(DeleteBehavior.SetNull);

              builder.HasMany(h => h.Departments)
                     .WithOne(d => d.Hospital)
                     .HasForeignKey(d => d.HospitalId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}