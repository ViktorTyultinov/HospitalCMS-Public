using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Users;
public class SpecialistProfile : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Specialty Specialty { get; set; }
    public required Guid? HospitalId { get; set; }
    public Locations.Hospital? Hospital { get; set; }
}