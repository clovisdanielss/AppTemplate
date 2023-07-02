using System.ComponentModel.DataAnnotations;

namespace AppTemplate.Models;

public class UsernameAndPassworModel
{
    [EmailAddress]
    public string UserName { get; set; }
    public string Password { get; set; }
}