using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.MedicalHistory;
public class Prescription : IEntity
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public DateTime PrescriptionDate { get; set; }
    public required PrescriptionStatus Status { get; set; }
    public required ICollection<PrescriptionLine> PrescriptionLines { get; set; } = [];
    public Guid GeneralPractitionerCheckUpId { get; set; }
    public GeneralPractitionerCheckUp? GeneralPractitionerCheckUp { get; set; }
}