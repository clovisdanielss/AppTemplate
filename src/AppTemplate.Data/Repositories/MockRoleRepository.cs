using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;

namespace AppTemplate.Data.Repositories
{
    public class MockRoleRepository : MockRepository<Role>, IRoleRepository
    {
        public MockRoleRepository()
        {
            if (repository.FirstOrDefault(x => x.Name == "User") == null)
            {
                repository.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "User"
                });
            }
            if (repository.FirstOrDefault(x => x.Name == "Tenant") == null)
            {
                repository.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Tenant"
                });
            }
            if (repository.FirstOrDefault(x => x.Name == "Admin") == null)
            {
                repository.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin"
                });
            }
        }

        public async Task<Role> GetRoleByName(string rolename)
        {
            return repository.FirstOrDefault(x => x.Name == rolename);
        }
    }
}
