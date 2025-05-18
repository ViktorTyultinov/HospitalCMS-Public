using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.MedicalHistory;
public class GeneralPractitionerCheckUp : IMedicalEvent
{
    public Guid Id { get; set; }
    public DateTime CheckupDate { get; set; }
    public string? Notes { get; set; }
    public required Guid DiagnosisId { get; set; }
    public Diagnosis? Diagnosis { get; set; }
    public required Guid PrescriptionId { get; set; }
    public Prescription? Prescription { get; set; }
    public required Guid PatientId { get; set; }
    public PatientProfile? Patient { get; set; }
    public required Guid GeneralPractitionerId { get; set; }
    public GeneralPractitionerProfile? GeneralPractitioner { get; set; }
    public DateTime CreatedAt { get; set; }
}