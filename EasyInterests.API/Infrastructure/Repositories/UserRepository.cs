using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async void Create(User user)
    {
      try
      {
        var createdUser = await _dbContext.Users.AddAsync(user);

        await _dbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao fazer esta operação.\n-> {e.Message}");
      }
    }

    public List<User> GetAll()
    {
      try
      {
        var users = _dbContext.Users.ToList();

        return users;
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public async Task<User> GetUser(int Id)
    {
      try
      {
        var user = await _dbContext.Users.FindAsync(Id);

        if (user is null)
        {
          throw new Exception("Not Found");
        }

        return user;
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public User GetUserByEmail(string Email)
    {
      try
      {
        var user = _dbContext.Users.SingleOrDefault(x => x.Email == Email);

        if (user is null)
        {
          throw new Exception("Not Found");
        }

        return user;
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public async void Update(int Id, User updatedUser)
    {
      try
      {
        var user = await _dbContext.Users.FindAsync(Id);

        if (user is null)
        {
          throw new Exception("Not Found");
        }

        _dbContext.Users.Update(updatedUser);

        await _dbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao fazer esta operação.\n-> {e.Message}");
      }
    }
  }
}
