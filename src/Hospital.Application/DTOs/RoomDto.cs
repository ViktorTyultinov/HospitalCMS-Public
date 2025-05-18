namespace Hospital.Application.DTOs;
public record RoomDto(Guid Id, string Name, string Description, string Address, int RoomNumber, int Floor);
