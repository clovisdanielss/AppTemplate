using AppTemplate.Application.Interfaces;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
