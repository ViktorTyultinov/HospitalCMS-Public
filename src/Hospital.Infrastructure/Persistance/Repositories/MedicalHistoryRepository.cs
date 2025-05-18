using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class MedicalHistoryRepository(HospitalDbContext context) : IMedicalHistoryRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(PatientMedicalHistory entity)
    {
        await _context.MedicalHistories.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<PatientMedicalHistory>> GetAllAsync() =>
        await _context.MedicalHistories.AsNoTracking().ToListAsync();

    public async Task<PatientMedicalHistory?> GetByIdAsync(Guid id) =>
        await _context.MedicalHistories.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Medical history with ID {id} not found.");
        _context.MedicalHistories.Remove(entity);
        return entity.Id;
    }

    public Task<PatientMedicalHistory> Update(PatientMedicalHistory entity)
    {
        _context.MedicalHistories.Update(entity);
        return Task.FromResult(entity);
    }
}