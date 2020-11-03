using System.Collections.Generic;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.Services;
using EasyInterests.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
  [ApiController]
  [Route("api/v1/users")]
  public class UserController : ControllerBase
  {
    // private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
      return Ok(_userRepository.GetAll());
    }
  }
}
