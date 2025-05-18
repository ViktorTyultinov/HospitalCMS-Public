using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Enums;

namespace Hospital.Domain.Entities.Users;

public class User
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required Gender Gender { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    

    // Navigation Properties and Foreign Keys
    public GeneralPractitionerProfile? GeneralPractitionerProfile { get; set; }
    public SpecialistProfile? SpecialistProfile { get; set; }
    public NurseProfile? NurseProfile { get; set; }
    public Guid? PatientProfileId { get; set; }
    public PatientProfile? PatientProfile { get; set; }
}