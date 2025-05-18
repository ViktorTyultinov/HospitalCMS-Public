namespace Hospital.Application.DTOs;
public record BedDto(Guid Id, int BedNumber, Guid RoomId, Guid? PatientId);