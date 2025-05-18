using Hospital.Domain.Entities.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class MedicalDeviceConfiguration : IEntityTypeConfiguration<MedicalDevice>
{
       public void Configure(EntityTypeBuilder<MedicalDevice> builder)
       {
              builder.HasKey(md => md.Id);

              builder.Property(md => md.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(md => md.SerialNumber)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(md => md.DeviceType)
                     .IsRequired()
                     .HasConversion<string>() // optional: store enum as string
                     .HasMaxLength(50);

              // Relationship: MedicalDevice belongs to one Hospital
              builder.HasOne(md => md.Hospital)
                     .WithMany(h => h.MedicalDevices)
                     .HasForeignKey(md => md.HospitalId)
                     .OnDelete(DeleteBehavior.SetNull);
       }
}