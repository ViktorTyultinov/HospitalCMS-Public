using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;

public class UserReposiotry(HospitalDbContext context) : IUserRepository
{
    private readonly HospitalDbContext _context = context;

    public async Task<Guid> AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Guid> Remove(Guid id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"User with ID {id} not found.");
        _context.Users.Remove(entity);
        return entity.Id;
    }

    public Task<User> Update(User entity)
    {
        _context.Users.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task<bool> IsNameAlreadyUsed(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}