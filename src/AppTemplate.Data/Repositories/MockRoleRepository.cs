using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Data.Repositories
{
    public class MockRoleRepository : MockRepository<Role>, IRoleRepository
    {
    }
}
