using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsNameAlreadyUsed(string username);
    Task<User?> GetUserByUsername(string username);
}