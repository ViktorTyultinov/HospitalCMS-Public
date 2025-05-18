using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class PrescriptionLineReposiotry(HospitalDbContext context) : IPrescriptionLineRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(PrescriptionLine entity)
    {
        await _context.PrescriptionLines.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<PrescriptionLine>> GetAllAsync() =>
        await _context.PrescriptionLines.AsNoTracking().ToListAsync();

    public async Task<PrescriptionLine?> GetByIdAsync(Guid id) =>
        await _context.PrescriptionLines.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Prescription line with ID {id} not found.");
        _context.PrescriptionLines.Remove(entity);
        return entity.Id;
    }

    public Task<PrescriptionLine> Update(PrescriptionLine entity)
    {
        _context.PrescriptionLines.Update(entity);
        return Task.FromResult(entity);
    }
}