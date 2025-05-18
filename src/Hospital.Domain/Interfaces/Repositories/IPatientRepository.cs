using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Interfaces.Repositories;
public interface IPatientRepository : IBaseRepository<PatientProfile> { }