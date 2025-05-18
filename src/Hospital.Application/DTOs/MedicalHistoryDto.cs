namespace Hospital.Application.DTOs;
public record MedicalHistoryDto(Guid Id, Guid PatientId, IEnumerable<Guid> EventIds);