using System.Collections.Generic;
using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.Models
{
  public class User
  {
    public User()
    {}
    public User(string name, string email, UserRolesEnum role, string phoneNumber)
    {
      this.Name = name;
      this.Email = email;
      this.Role = role;
      this.PhoneNumber = phoneNumber;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRolesEnum Role { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<Debt>? DebtsAsCustomer { get; set; }
    public virtual ICollection<Debt>? DebtsAsNegotiator { get; set; }
  }

  public class Auth
  {
    public string Email { get; set; }
  }
}
