using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models;

public class UserRole : IEntity{
    public Guid Id { get; set; }
    public string RoleId { get; set; }
    public string UserId { get; set; }
}