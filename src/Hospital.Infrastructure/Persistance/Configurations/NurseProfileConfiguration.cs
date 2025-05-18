using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class NurseConfiguration : IEntityTypeConfiguration<NurseProfile>
{
    public void Configure(EntityTypeBuilder<NurseProfile> builder)
    {
        builder.HasKey(n => n.Id);

        builder.HasOne(n => n.Hospital)
               .WithMany(h => h.Nurses)
               .HasForeignKey(n => n.HospitalId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
