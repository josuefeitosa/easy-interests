using System.Collections.Generic;
using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.Models
{
    public class User
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public UserRolesEnum Role { get; set; }
      public string Password { get; set; }
    }

    public class Auth
    {
      public string Email { get; set; }
    }
}
