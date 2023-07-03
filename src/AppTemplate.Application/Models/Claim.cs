using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models
{
    public class Claim : IEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
    }
}
