using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class SpecialistConfiguration : IEntityTypeConfiguration<SpecialistProfile>
{
    public void Configure(EntityTypeBuilder<SpecialistProfile> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Hospital)
               .WithMany(h => h.Specialists)
               .HasForeignKey(s => s.HospitalId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}