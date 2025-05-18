using Hospital.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class BedConfiguration : IEntityTypeConfiguration<Bed>
{
       public void Configure(EntityTypeBuilder<Bed> builder)
       {
              builder.HasKey(b => b.Id);

              builder.Property(b => b.BedNumber)
                     .IsRequired();

              builder.HasOne(b => b.Room)
                     .WithMany(r => r.Beds)
                     .HasForeignKey(b => b.RoomId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(b => b.Patient)
                     .WithMany()
                     .HasForeignKey(b => b.PatientId)
                     .OnDelete(DeleteBehavior.SetNull);

              builder.HasIndex(b => new { b.RoomId, b.BedNumber }).IsUnique();
       }
}