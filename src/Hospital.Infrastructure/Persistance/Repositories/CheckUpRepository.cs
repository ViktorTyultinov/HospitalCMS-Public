using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class CheckUpReposiotry(HospitalDbContext context) : IGeneralPractitionerCheckUpRepository
{
    private readonly HospitalDbContext _context = context;

    public async Task<Guid> AddAsync(GeneralPractitionerCheckUp entity)
    {
        await _context.GeneralPractitionerCheckUps.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<GeneralPractitionerCheckUp>> GetAllAsync() =>
        await _context.GeneralPractitionerCheckUps.AsNoTracking().ToListAsync();

    public async Task<GeneralPractitionerCheckUp?> GetByIdAsync(Guid id) =>
        await _context.GeneralPractitionerCheckUps.AsNoTracking().FirstOrDefaultAsync(gpc => gpc.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id) ??
            throw new KeyNotFoundException($"Checkup with ID ${id} not found");
        _context.GeneralPractitionerCheckUps.Remove(entity);
        return entity.Id;
    }

    public Task<GeneralPractitionerCheckUp> Update(GeneralPractitionerCheckUp entity)
    {
        _context.GeneralPractitionerCheckUps.Update(entity);
        return Task.FromResult(entity);
    }
}