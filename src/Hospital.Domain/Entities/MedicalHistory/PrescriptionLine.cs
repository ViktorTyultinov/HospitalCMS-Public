using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.MedicalHistory;
public class PrescriptionLine : IEntity
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string MedicationName { get; set; }
    public required string Dosage { get; set; }
    public required string Frequency { get; set; }
    public string? Instructions { get; set; }
    public int Duration { get; set; }
    public Guid PrescriptionId { get; set; }
    public Prescription? Prescription { get; set; }
}