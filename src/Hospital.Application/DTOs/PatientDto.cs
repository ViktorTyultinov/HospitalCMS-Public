namespace Hospital.Application.DTOs;
public record PatientDto(Guid Id, string FirstName, string LastName, DateTime DateOfBirth, int Gender, string Username);