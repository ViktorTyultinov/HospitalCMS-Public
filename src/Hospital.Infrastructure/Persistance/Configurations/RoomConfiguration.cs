using Hospital.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistance.Configurations;
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
       public void Configure(EntityTypeBuilder<Room> builder)
       {
              builder.HasKey(r => r.Id);

              builder.Property(l => l.Name)
                     .IsRequired()
                     .HasMaxLength(200);

              builder.Property(l => l.Description)
                     .IsRequired()
                     .HasMaxLength(500);

              builder.Property(l => l.Address)
                     .IsRequired()
                     .HasMaxLength(300);

              builder.Property(r => r.RoomNumber).IsRequired();
              builder.Property(r => r.Floor).IsRequired();

              builder.HasOne(r => r.Department)
                     .WithMany(d => d.Rooms)
                     .HasForeignKey(r => r.DepartmentId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}