namespace Hospital.Domain.Interfaces.BaseInterfaces;
public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T entity);
    Task<T> Update(T entity);
    Task<Guid> Remove(Guid Id);
}