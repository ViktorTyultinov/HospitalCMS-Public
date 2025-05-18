namespace Hospital.Application.DTOs;
public record NurseDto(Guid Id, string FirstName, string LastName, DateTime DateOfBirth, int Gender, string Username);