namespace AppTemplate.Shared.Interfaces;
public interface IRepository<T> : IDisposable
{
    Task<T> GetById(string id);
    Task Update(T entity);
    Task DeleteById(string id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T item);
}