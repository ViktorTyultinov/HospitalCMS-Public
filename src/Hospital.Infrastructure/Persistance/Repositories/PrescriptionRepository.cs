using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class PrescriptionReposiotry(HospitalDbContext context) : IPrescriptionRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Prescription entity)
    {
        await _context.Prescriptions.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Prescription>> GetAllAsync() =>
        await _context.Prescriptions.AsNoTracking().ToListAsync();

    public async Task<Prescription?> GetByIdAsync(Guid id) =>
        await _context.Prescriptions.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Prescription with ID {id} not found.");
        _context.Prescriptions.Remove(entity);
        return entity.Id;
    }

    public Task<Prescription> Update(Prescription entity)
    {
        _context.Prescriptions.Update(entity);
        return Task.FromResult(entity);
    }
}