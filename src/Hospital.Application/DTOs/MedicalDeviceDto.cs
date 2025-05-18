namespace Hospital.Application.DTOs;
public record MedicalDeviceDto(Guid Id, string Name, string SerialNumber, string DeviceType, Guid HospitalId, Guid? StorageUnitId);