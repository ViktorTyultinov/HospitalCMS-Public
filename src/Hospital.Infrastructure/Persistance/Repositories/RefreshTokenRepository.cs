using Hospital.Domain.Entities.Authentication;
using Hospital.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistance.Repositories;

public class RefreshTokenReposiotry(HospitalDbContext context) : IRefreshTokenRepository
{
    private readonly HospitalDbContext _context = context;

    public async Task<Guid> AddAsync(RefreshToken entity)
    {
        await _context.RefreshTokens.AddAsync(entity);
        return entity.Id;
    }

    public Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        throw new NotImplementedException("This method should not be implemented. There is absolutely no reason to get all tokens");
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id) =>
        await _context.RefreshTokens
            .OrderByDescending(rt => rt.ExpiresAt)
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.UserId == id);

    public Task<Guid> Remove(Guid id)
    {
        throw new NotImplementedException("This method should not be implemented. This is done via a chronjob");
    }

    public async Task<int> RemoveExpiredTokens()
    {
        var removedTokens = await _context.RefreshTokens.Where(rt => rt.ExpiresAt < DateTime.UtcNow).ExecuteDeleteAsync();
        return removedTokens;
    }

    public Task<RefreshToken> Update(RefreshToken entity)
    {
        throw new NotImplementedException("This method should not be implemented. Do not update refresh tokens. Create new ones");
    }
}