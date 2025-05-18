using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class MedicalHistoryConfiguration : IEntityTypeConfiguration<PatientMedicalHistory>
{
    public void Configure(EntityTypeBuilder<PatientMedicalHistory> builder)
    {
        builder.HasKey(mh => mh.Id);

        builder.HasOne(mh => mh.Patient)
               .WithOne(p => p.MedicalHistory)
               .HasForeignKey<PatientProfile>(pp => pp.MedicalHistoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}