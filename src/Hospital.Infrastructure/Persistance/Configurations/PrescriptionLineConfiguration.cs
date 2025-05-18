using Hospital.Domain.Entities.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class PrescriptionLineConfiguration : IEntityTypeConfiguration<PrescriptionLine>
{
       public void Configure(EntityTypeBuilder<PrescriptionLine> builder)
       {
              builder.HasKey(pl => pl.Id);

              builder.Property(pl => pl.MedicationName)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(pl => pl.Dosage)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(pl => pl.Frequency)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(pl => pl.Instructions)
                     .HasMaxLength(500);

              builder.Property(pl => pl.Duration)
                     .IsRequired();

              builder.HasOne(pl => pl.Prescription)
                     .WithMany(p => p.PrescriptionLines)
                     .HasForeignKey(pl => pl.PrescriptionId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}