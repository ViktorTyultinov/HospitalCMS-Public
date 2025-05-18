using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Users;
public class NurseProfile : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public required Guid? HospitalId { get; set; }
    public Locations.Hospital? Hospital { get; set; }
}