using Hospital.Domain.Entities.Devices;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class MedicalDeviceReposiotry(HospitalDbContext context) : IMedicalDeviceRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(MedicalDevice entity)
    {
        await _context.MedicalDevices.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<MedicalDevice>> GetAllAsync() =>
        await _context.MedicalDevices.AsNoTracking().ToListAsync();

    public async Task<MedicalDevice?> GetByIdAsync(Guid id) =>
        await _context.MedicalDevices.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Medical device with ID {id} not found.");
        _context.MedicalDevices.Remove(entity);
        return entity.Id;
    }

    public Task<MedicalDevice> Update(MedicalDevice entity)
    {
        _context.MedicalDevices.Update(entity);
        return Task.FromResult(entity);
    }
}