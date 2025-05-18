using System.ComponentModel.DataAnnotations;

namespace Hospital.Domain.Entities.Locations;
public class Room
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public ICollection<Bed>? Beds { get; set; } = [];
    public required Guid DepartmentId { get; set; }
    public Department? Department { get; set; }
}