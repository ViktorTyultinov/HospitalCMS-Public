using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class DiagnosisReposiotry(HospitalDbContext context) : IDiagnosisRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Diagnosis entity)
    {
        await _context.Diagnoses.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Diagnosis>> GetAllAsync() =>
        await _context.Diagnoses.AsNoTracking().ToListAsync();

    public async Task<Diagnosis?> GetByIdAsync(Guid id) =>
        await _context.Diagnoses.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Diagnosis with ID {id} not found.");
        _context.Diagnoses.Remove(entity);
        return entity.Id;
    }

    public Task<Diagnosis> Update(Diagnosis entity)
    {
        _context.Diagnoses.Update(entity);
        return Task.FromResult(entity);
    }
}