using Hospital.Domain.Entities.Locations;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;
public class RoomReposiotry(HospitalDbContext context) : IRoomRepository
{
    private readonly HospitalDbContext _context = context;
    public async Task<Guid> AddAsync(Room entity)
    {
        await _context.Rooms.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<Room>> GetAllAsync() =>
        await _context.Rooms.AsNoTracking().ToListAsync();

    public async Task<Room?> GetByIdAsync(Guid id) =>
        await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Room with ID {id} not found.");
        _context.Rooms.Remove(entity);
        return entity.Id;
    }

    public Task<Room> Update(Room entity)
    {
        _context.Rooms.Update(entity);
        return Task.FromResult(entity);
    }
}