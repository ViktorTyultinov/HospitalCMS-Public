using Hospital.Domain.Entities.Locations;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class StorageUnitReposiotry(HospitalDbContext context) : IStorageUnitRepository
{
    private readonly HospitalDbContext _context = context;

    public async Task<Guid> AddAsync(StorageUnit entity)
    {
        await _context.StorageUnits.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<StorageUnit>> GetAllAsync() =>
        await _context.StorageUnits.AsNoTracking().ToListAsync();

    public async Task<StorageUnit?> GetByIdAsync(Guid id) =>
        await _context.StorageUnits.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Storage unit with ID {id} not found.");
        _context.StorageUnits.Remove(entity);
        return entity.Id;
    }

    public Task<StorageUnit> Update(StorageUnit entity)
    {
        _context.StorageUnits.Update(entity);
        return Task.FromResult(entity);
    }
}