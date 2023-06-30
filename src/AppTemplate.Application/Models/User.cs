using AppTemplate.Application.Interfaces;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
