using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Entities.Devices;

namespace Hospital.Domain.Entities.Locations;
public class StorageUnit
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public ICollection<MedicalDevice>? MedicalDevices { get; set; } = [];
    public required Guid DepartmentId { get; set; }
    public Department? Department { get; set; }
}