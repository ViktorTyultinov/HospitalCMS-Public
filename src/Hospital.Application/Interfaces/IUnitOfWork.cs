using Hospital.Domain.Interfaces.Repositories;

namespace Hospital.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBedRepository Beds { get; }
    IGeneralPractitionerCheckUpRepository GeneralPractitionerCheckUps { get; }
    IDepartmentRepository Departments { get; }
    IDiagnosisRepository Diagnoses { get; }
    IGeneralPractitionerRepository GeneralPractitioners { get; }
    IHospitalRepository Hospitals { get; }
    IMedicalDeviceRepository MedicalDevices { get; }
    INurseRepository Nurses { get; }
    IPatientRepository Patients { get; }
    IPrescriptionLineRepository PrescriptionLines { get; }
    IPrescriptionRepository Prescriptions { get; }
    IRoomRepository Rooms { get; }
    IStorageUnitRepository StorageUnits { get; }
    ISpecialistRepository Specialists { get; }
    IMedicalHistoryRepository MedicalHistories { get; }
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    Task<int> CompleteAsync();
}
