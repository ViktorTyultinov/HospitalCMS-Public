using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Entities.Devices;
using Hospital.Domain.Entities.Users;

namespace Hospital.Domain.Entities.Locations;
public class Hospital
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string Address { get; set; }
    public ICollection<GeneralPractitionerProfile> GeneralPractitioners { get; set; } = [];
    public ICollection<SpecialistProfile> Specialists { get; set; } = [];
    public ICollection<NurseProfile> Nurses { get; set; } = [];
    public ICollection<MedicalDevice> MedicalDevices { get; set; } = [];
    public ICollection<Department> Departments { get; set; } = [];
}