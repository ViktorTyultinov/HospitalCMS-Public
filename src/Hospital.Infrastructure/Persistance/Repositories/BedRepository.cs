using Hospital.Domain.Entities.Locations;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class BedRepository(HospitalDbContext context) : IBedRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Bed entity)
    {
        await _context.Beds.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Bed>> GetAllAsync() =>
        await _context.Beds.AsNoTracking().ToListAsync();

    public async Task<Bed?> GetByIdAsync(Guid id) =>
        await _context.Beds.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id) ??
            throw new KeyNotFoundException($"Bed with ID {id} not found.");
        _context.Beds.Remove(entity);
        return entity.Id;
    }

    public Task<Bed> Update(Bed entity)
    {
        _context.Beds.Update(entity);
        return Task.FromResult(entity);
    }
}