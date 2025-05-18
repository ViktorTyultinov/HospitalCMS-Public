namespace Hospital.Application.DTOs;
public record DepartmentDto(Guid Id, string Name, string Description, string Address, List<StorageUnitDto> StorageUnits, List<RoomDto> Rooms);
