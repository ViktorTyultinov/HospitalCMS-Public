namespace Hospital.Application.DTOs;
public record HospitalDto(Guid Id, string Name, string Address, List<GeneralPractitionerDto> GeneralPractitioners, List<DepartmentDto> Departments);