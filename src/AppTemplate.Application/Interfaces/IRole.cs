using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Application.Interfaces
{
    public interface IRole<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
    }
}
