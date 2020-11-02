using System.Collections.Generic;
using System.Linq;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Infrastructure.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly EasyInterestsDBContext _dbContext;

    public UserRepository(EasyInterestsDBContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Create()
    {
      throw new System.NotImplementedException();
    }

    public List<User> GetAll()
    {
      return _dbContext.Users.ToList();
    }

    public User GetUser(int Id)
    {
      throw new System.NotImplementedException();
    }

    public User GetUserByEmail(string Email)
    {
      throw new System.NotImplementedException();
    }

    public void Update(int Id)
    {
      throw new System.NotImplementedException();
    }
  }
}
