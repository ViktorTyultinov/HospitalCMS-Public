namespace Hospital.Application.DTOs;
public record SpecialistDto(Guid Id, string FirstName, string LastName, DateTime DateOfBirth, int Gender, string Username, int Specialty);