using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.MedicalHistory;
public class Diagnosis : IEntity
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string DiagnosisName { get; set; }
    public string? Description { get; set; }
    public required Guid GeneralPractitionerCheckUpId { get; set; }
    public GeneralPractitionerCheckUp? GeneralPractitionerCheckUp { get; set; }
}