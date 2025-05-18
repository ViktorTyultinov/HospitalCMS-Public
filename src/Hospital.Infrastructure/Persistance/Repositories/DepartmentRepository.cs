using Hospital.Domain.Entities.Locations;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class DepartmentRepository(HospitalDbContext context) : IDepartmentRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Department entity)
    {
        await _context.Departments.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Department>> GetAllAsync() =>
        await _context.Departments.AsNoTracking().ToListAsync();

    public async Task<Department?> GetByIdAsync(Guid id) =>
        await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id) ??
            throw new KeyNotFoundException($"Hospital with ID {id} not found.");
        _context.Departments.Remove(entity);
        return entity.Id;
    }

    public Task<Department> Update(Department entity)
    {
        _context.Departments.Update(entity);
        return Task.FromResult(entity);
    }
}