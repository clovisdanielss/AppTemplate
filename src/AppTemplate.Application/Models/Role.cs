using AppTemplate.Application.Interfaces;
using AppTemplate.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Application.Models
{
    public class Role : IEntity, IRole<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
