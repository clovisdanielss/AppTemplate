namespace AppTemplate.Shared.Interfaces;
public interface IRepository<T> : IDisposable
{
    Task<T> GetById(Guid id);
    Task Update(T entity);
    Task DeleteById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T item);
}