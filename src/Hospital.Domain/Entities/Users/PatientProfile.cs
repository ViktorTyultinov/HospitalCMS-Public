using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Users;
public class PatientProfile : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public required Guid? MedicalHistoryId { get; set; }
    public MedicalHistory.PatientMedicalHistory? MedicalHistory { get; set; }
    public required Guid? GeneralPractitionerProfileId { get; set; }
    public GeneralPractitionerProfile? GeneralPractitionerProfile { get; set; }
}