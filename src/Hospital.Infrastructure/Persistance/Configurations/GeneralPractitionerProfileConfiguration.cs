using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class GeneralPractitionerConfiguration : IEntityTypeConfiguration<GeneralPractitionerProfile>
{
       public void Configure(EntityTypeBuilder<GeneralPractitionerProfile> builder)
       {
              builder.HasKey(gp => gp.Id);

              builder.Property(gp => gp.HospitalId).IsRequired();
              builder.HasOne(gp => gp.Hospital)
                     .WithMany() // assuming Hospital doesn't have a collection of GPs
                     .HasForeignKey(gp => gp.HospitalId)
                     .OnDelete(DeleteBehavior.SetNull);

              builder.HasMany(gp => gp.Patients)
                     .WithOne(p => p.GeneralPractitionerProfile)  // PatientProfile will have GP (one-to-many)
                     .HasForeignKey(p => p.GeneralPractitionerProfileId)
                     .OnDelete(DeleteBehavior.SetNull); // or your preferred delete behavior
       }
}