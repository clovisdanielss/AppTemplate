using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;

namespace AppTemplate.Data.Repositories
{
    public class MockUserRepository : MockRepository<User>, IUserRepository
    {
        public async Task<User> GetByEmail(string email)
        {
            return repository.FirstOrDefault(user => user.Email == email);
        }

        public async Task<User> GetByUsername(string username)
        {
            return repository.FirstOrDefault(user => user.UserName == username);
        }
    }
}
