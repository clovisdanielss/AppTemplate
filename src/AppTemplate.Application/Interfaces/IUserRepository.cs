﻿using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Application.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
    }
}
