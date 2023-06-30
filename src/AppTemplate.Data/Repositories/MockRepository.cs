using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Data.Repositories
{
    public class MockRepository<T> : IRepository<T> where T : IEntity, new()
    {
        public static List<T> repository = new List<T>();
        public MockRepository()
        {

        }
        public async Task Add(T item)
        {
            item.Id = Guid.NewGuid();
            repository.Add(item);
        }

        public async Task DeleteById(Guid id)
        {
            var toDelete = repository.FirstOrDefault(i => i.Id == id);
            if (toDelete != null)
            {
                repository.Remove(toDelete);
            }
        }

        public void Dispose()
        {
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return repository.ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            return repository.FirstOrDefault(i => i.Id == id);
        }

        public async Task Update(T entity)
        {
            await DeleteById(entity.Id);
            await Add(entity);
        }
    }
}
