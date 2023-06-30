using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;

namespace AppTemplate.Data.Repositories
{
    public class MockUserRepository : MockRepository<User>, IUserRepository
    {
    }
}
