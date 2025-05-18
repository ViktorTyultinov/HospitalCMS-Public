namespace Hospital.Application.DTOs;
public record DiagnosisDto(Guid Id, string Name, string Description, Guid PatientId);

