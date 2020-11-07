using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;
using EasyInterests.API.Infrastructure.Repositories;

namespace EasyInterests.API.Application.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IDebtRepository _debtRepository;
    public UserService(IUserRepository userRepository, IDebtRepository debtRepository)
    {
      _userRepository = userRepository;
      _debtRepository = debtRepository;
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

    public List<UserListViewModel> GetAll()
    {
      var users = _userRepository.GetAll();

      return users.Select(x => new UserListViewModel {
        Id = x.Id,
        Name = x.Name,
        Email = x.Email,
        PhoneNumber = x.PhoneNumber,
        Role = x.Role.ToString()
      }).ToList();
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
      _userRepository.Update(Id, updatedUser);
    }
  }
}
