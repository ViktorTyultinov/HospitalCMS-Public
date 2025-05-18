using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Devices;
public class MedicalDevice : IEntity
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string SerialNumber { get; set; }
    public required DeviceType DeviceType { get; set; }
    public required Guid HospitalId { get; set; }
    public Locations.Hospital? Hospital { get; set; }
}