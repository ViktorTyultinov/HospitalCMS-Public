using Hospital.Application.Interfaces;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure.Persistance;

public class UnitOfWork(HospitalDbContext context, IServiceProvider serviceProvider) : IUnitOfWork, IDisposable
{
    private readonly HospitalDbContext _context = context;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private IBedRepository? _beds;
    public IBedRepository Beds => _beds ??= _serviceProvider.GetRequiredService<IBedRepository>();

    private IGeneralPractitionerCheckUpRepository? _generalPractitionerCheckUps;
    public IGeneralPractitionerCheckUpRepository GeneralPractitionerCheckUps => _generalPractitionerCheckUps ??= _serviceProvider.GetRequiredService<IGeneralPractitionerCheckUpRepository>();

    private IDepartmentRepository? _departments;
    public IDepartmentRepository Departments => _departments ??= _serviceProvider.GetRequiredService<IDepartmentRepository>();

    private IDiagnosisRepository? _diagnoses;
    public IDiagnosisRepository Diagnoses => _diagnoses ??= _serviceProvider.GetRequiredService<IDiagnosisRepository>();

    private IGeneralPractitionerRepository? _generalPractitioners;
    public IGeneralPractitionerRepository GeneralPractitioners => _generalPractitioners ??= _serviceProvider.GetRequiredService<IGeneralPractitionerRepository>();

    private ISpecialistRepository? _specialists;
    public ISpecialistRepository Specialists => _specialists ??= _serviceProvider.GetRequiredService<ISpecialistRepository>();

    private IHospitalRepository? _hospitals;
    public IHospitalRepository Hospitals => _hospitals ??= _serviceProvider.GetRequiredService<IHospitalRepository>();

    private IMedicalDeviceRepository? _medicalDevices;
    public IMedicalDeviceRepository MedicalDevices => _medicalDevices ??= _serviceProvider.GetRequiredService<IMedicalDeviceRepository>();

    private INurseRepository? _nurses;
    public INurseRepository Nurses => _nurses ??= _serviceProvider.GetRequiredService<INurseRepository>();

    private IPatientRepository? _patients;
    public IPatientRepository Patients => _patients ??= _serviceProvider.GetRequiredService<IPatientRepository>();

    private IPrescriptionRepository? _prescriptions;
    public IPrescriptionRepository Prescriptions => _prescriptions ??= _serviceProvider.GetRequiredService<IPrescriptionRepository>();

    private IPrescriptionLineRepository? _prescriptionLines;
    public IPrescriptionLineRepository PrescriptionLines => _prescriptionLines ??= _serviceProvider.GetRequiredService<IPrescriptionLineRepository>();

    private IRoomRepository? _rooms;
    public IRoomRepository Rooms => _rooms ??= _serviceProvider.GetRequiredService<IRoomRepository>();

    private IStorageUnitRepository? _storageUnits;
    public IStorageUnitRepository StorageUnits => _storageUnits ??= _serviceProvider.GetRequiredService<IStorageUnitRepository>();

    private IMedicalHistoryRepository? _medicalHistories;
    public IMedicalHistoryRepository MedicalHistories => _medicalHistories ??= _serviceProvider.GetRequiredService<IMedicalHistoryRepository>();

    private IUserRepository? _users;
    public IUserRepository Users => _users ??= _serviceProvider.GetRequiredService<IUserRepository>();

    private IRefreshTokenRepository? _refreshTokens;
    public IRefreshTokenRepository RefreshTokens => _refreshTokens ??= _serviceProvider.GetRequiredService<IRefreshTokenRepository>();

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}