using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class HospitalReposiotry(HospitalDbContext context) : IHospitalRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Domain.Entities.Locations.Hospital entity)
    {
        await _context.Hospitals.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Domain.Entities.Locations.Hospital>> GetAllAsync() =>
        await _context.Hospitals.AsNoTracking().ToListAsync();

    public async Task<Domain.Entities.Locations.Hospital?> GetByIdAsync(Guid id) =>
        await _context.Hospitals.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Hospital with ID {id} not found.");
        _context.Hospitals.Remove(entity);
        return entity.Id;
    }

    public Task<Domain.Entities.Locations.Hospital> Update(Domain.Entities.Locations.Hospital entity)
    {
        _context.Hospitals.Update(entity);
        return Task.FromResult(entity);
    }
}