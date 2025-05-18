namespace Hospital.Application.DTOs;
public record PrescriptionDto(Guid Id, DateTime PrescriptionDate, string Status, Guid GeneralPractitionerCheckUpId);
