using System.Collections.Generic;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Application.Services
{
    public interface IUserService
    {
      List<User> GetAll();
      User GetUser(int Id);
      User GetUserByEmail(string Email);
      void Create();
      void Update(int Id);
    }
}
