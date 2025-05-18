using Hospital.Domain.Entities.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
       public void Configure(EntityTypeBuilder<Prescription> builder)
       {
              builder.HasKey(p => p.Id);

              builder.Property(p => p.PrescriptionDate)
                     .IsRequired();

              builder.Property(p => p.Status)
                     .IsRequired()
                     .HasConversion<string>() // Optional: store enum as string
                     .HasMaxLength(50);

              builder.HasMany(p => p.PrescriptionLines)
                     .WithOne(pl => pl.Prescription)
                     .HasForeignKey(pl => pl.PrescriptionId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(p => p.GeneralPractitionerCheckUp)
                     .WithOne(c => c.Prescription)
                     .HasForeignKey<Prescription>(p => p.GeneralPractitionerCheckUpId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}