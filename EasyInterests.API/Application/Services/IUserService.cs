using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;

namespace EasyInterests.API.Application.Services
{
    public interface IUserService
    {
      List<UserListViewModel> GetAll();
      User GetUser(int Id);
      User GetUserByEmail(string Email);
      void Create(CreateUserDTO user);
      void Update(int Id, UpdateUserDTO updatedUser);
    }
}
