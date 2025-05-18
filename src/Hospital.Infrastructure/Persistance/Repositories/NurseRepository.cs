using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class NurseReposiotry(HospitalDbContext context) : INurseRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(NurseProfile entity)
    {
        await _context.NurseProfiles.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<NurseProfile>> GetAllAsync() =>
        await _context.NurseProfiles.AsNoTracking().ToListAsync();

    public async Task<NurseProfile?> GetByIdAsync(Guid id) =>
        await _context.NurseProfiles.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Hospital with ID {id} not found.");
        _context.NurseProfiles.Remove(entity);
        return entity.Id;
    }

    public Task<NurseProfile> Update(NurseProfile entity)
    {
        _context.NurseProfiles.Update(entity);
        return Task.FromResult(entity);
    }
}