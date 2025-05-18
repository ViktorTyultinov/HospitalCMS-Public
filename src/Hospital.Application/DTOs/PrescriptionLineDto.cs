namespace Hospital.Application.DTOs;
public record PrescriptionLineDto(Guid Id, string MedicationName, string Dosage, string Frequency, string? Instructions, int Duration, Guid PrescriptionId);