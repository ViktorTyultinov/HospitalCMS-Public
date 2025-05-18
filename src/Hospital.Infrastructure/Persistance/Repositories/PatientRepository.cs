using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class PatientReposiotry(HospitalDbContext context) : IPatientRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(PatientProfile entity)
    {
        await _context.PatientProfiles.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<PatientProfile>> GetAllAsync() =>
        await _context.PatientProfiles.AsNoTracking().ToListAsync();

    public async Task<PatientProfile?> GetByIdAsync(Guid id) =>
        await _context.PatientProfiles.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Patient with ID {id} not found.");
        _context.PatientProfiles.Remove(entity);
        return entity.Id;
    }

    public Task<PatientProfile> Update(PatientProfile entity)
    {
        _context.PatientProfiles.Update(entity);
        return Task.FromResult(entity);
    }
}