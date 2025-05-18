using System.ComponentModel.DataAnnotations;

namespace Hospital.Domain.Entities.Locations;
public class Department
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public required ICollection<Room> Rooms { get; set; } = [];
    public required ICollection<StorageUnit> StorageUnits { get; set; } = [];
    public required Guid HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
}