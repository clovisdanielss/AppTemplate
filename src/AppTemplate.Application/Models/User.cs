using AppTemplate.Application.Interfaces;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models
{
    public class User : IEntity, IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string ConfirmedEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
