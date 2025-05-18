using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Enums;

namespace Hospital.API.Authentication;

public record CreateUserDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Username,
    [Required] string Password,
    [Required, EmailAddress] string Email,
    [Required] DateOnly DoB,
    [Required, EnumDataType(typeof(Gender))] Gender Gender);
