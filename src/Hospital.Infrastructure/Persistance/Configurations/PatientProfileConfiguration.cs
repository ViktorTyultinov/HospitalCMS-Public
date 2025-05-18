using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class PatientProfileConfiguration : IEntityTypeConfiguration<PatientProfile>
{
    public void Configure(EntityTypeBuilder<PatientProfile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.MedicalHistory)
               .WithOne()
               .HasForeignKey<PatientProfile>(p => p.MedicalHistoryId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.GeneralPractitionerProfile)
               .WithMany(gp => gp.Patients)
               .HasForeignKey(p => p.GeneralPractitionerProfileId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
