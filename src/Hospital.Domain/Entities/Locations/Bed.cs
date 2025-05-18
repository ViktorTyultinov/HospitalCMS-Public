using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Locations;
public class Bed : IEntity
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public int BedNumber { get; set; }
    public required Guid RoomId { get; set; }
    public Room? Room { get; set; }
    public Guid? PatientId { get; set; }
    public PatientProfile? Patient { get; set; }
}