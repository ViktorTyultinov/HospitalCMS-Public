namespace Hospital.Application.DTOs;
public record StorageUnitDto(Guid Id, string Name, string Description, string Address, Guid DepartmentId);