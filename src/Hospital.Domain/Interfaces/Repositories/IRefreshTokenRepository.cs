using Hospital.Domain.Entities.Authentication;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    Task<int> RemoveExpiredTokens();
}