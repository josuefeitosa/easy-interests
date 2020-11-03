using System.Collections.Generic;
using System.Threading.Tasks;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Infrastructure.Repositories
{
    public interface IUserRepository
    {
      List<User> GetAll();
      Task<User> GetUser(int Id);
      User GetUserByEmail(string Email);
      void Create(User user);
      void Update(int Id, User updatedUser);
    }
}
