using System.Collections.Generic;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Infrastructure.Repositories
{
    public interface IUserRepository
    {
      List<User> GetAll();
      User GetUser(int Id);
      User GetUserByEmail(string Email);
      void Create();
      void Update(int Id);
    }
}
