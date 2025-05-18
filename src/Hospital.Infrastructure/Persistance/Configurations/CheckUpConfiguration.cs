using Hospital.Domain.Entities.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class CheckUpConfiguration : IEntityTypeConfiguration<GeneralPractitionerCheckUp>
{
       public void Configure(EntityTypeBuilder<GeneralPractitionerCheckUp> builder)
       {
              builder.HasKey(c => c.Id);

              builder.Property(c => c.CheckupDate)
                     .IsRequired();

              builder.Property(c => c.Notes)
                     .HasMaxLength(500);

              // Relationship: Each CheckUp has one Diagnosis
              builder.HasOne(c => c.Diagnosis)
                     .WithOne(d => d.GeneralPractitionerCheckUp)
                     .HasForeignKey<GeneralPractitionerCheckUp>(c => c.DiagnosisId)
                     .OnDelete(DeleteBehavior.Cascade);

              // Relationship: Each CheckUp has one Prescription
              builder.HasOne(c => c.Prescription)
                     .WithOne(p => p.GeneralPractitionerCheckUp)
                     .HasForeignKey<GeneralPractitionerCheckUp>(c => c.PrescriptionId)
                     .OnDelete(DeleteBehavior.Cascade);

              // Relationship: Each CheckUp is made by one Doctor
              builder.HasOne(c => c.GeneralPractitioner)
                     .WithMany()
                     .HasForeignKey(c => c.GeneralPractitionerId)
                     .OnDelete(DeleteBehavior.NoAction);
       }
}