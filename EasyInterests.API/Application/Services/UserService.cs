using System.Collections.Generic;
using System.Threading.Tasks;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Infrastructure.Repositories;

namespace EasyInterests.API.Application.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public void Create(CreateUserDTO user)
    {
      var userObj = new User() {
        Name = user.Name,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        Role = user.Role
      };

      _userRepository.Create(userObj);
    }

    public List<User> GetAll()
    {
      return _userRepository.GetAll();
    }

    public User GetUser(int Id)
    {
      return _userRepository.GetUser(Id).Result;
    }

    public User GetUserByEmail(string Email)
    {
      return _userRepository.GetUserByEmail(Email);
    }

    public void Update(int Id, UpdateUserDTO updatedUser)
    {
      var userObj = new User();

      if (updatedUser.Name != null)
        userObj.Name = updatedUser.Name;

      if (updatedUser.Email != null)
        userObj.Email = updatedUser.Email;

      if (updatedUser.PhoneNumber != null)
        userObj.PhoneNumber = updatedUser.PhoneNumber;

      if (updatedUser.Role != 0)
        userObj.Role = updatedUser.Role;

      _userRepository.Update(Id, userObj);
    }
  }
}
