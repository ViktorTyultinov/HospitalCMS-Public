using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class SpecialistRepository(HospitalDbContext context) : ISpecialistRepository
{
    private readonly HospitalDbContext _context = context;

    public async Task<Guid> AddAsync(SpecialistProfile entity)
    {
        await _context.SpecialistProfiles.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<SpecialistProfile>> GetAllAsync() =>
        await _context.SpecialistProfiles.AsNoTracking().ToListAsync();

    public async Task<SpecialistProfile?> GetByIdAsync(Guid id) =>
        await _context.SpecialistProfiles.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id) ??
            throw new KeyNotFoundException($"Specialist with ID {id} not found.");
        _context.SpecialistProfiles.Remove(entity);
        return entity.Id;
    }

    public Task<SpecialistProfile> Update(SpecialistProfile entity)
    {
        _context.SpecialistProfiles.Update(entity);
        return Task.FromResult(entity);
    }
}