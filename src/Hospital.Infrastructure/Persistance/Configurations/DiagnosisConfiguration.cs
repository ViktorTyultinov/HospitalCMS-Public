using Hospital.Domain.Entities.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.DiagnosisName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(d => d.Description)
               .HasMaxLength(500);

        // One-to-One relationship: Each CheckUp has one Diagnosis
        builder.HasOne(d => d.GeneralPractitionerCheckUp)
               .WithOne(c => c.Diagnosis)
               .HasForeignKey<Diagnosis>(d => d.GeneralPractitionerCheckUpId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}