using System;
using System.Threading.Tasks;
using AuthenticationJWT.Helpers;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.Services;
using EasyInterests.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/api/v1/auth")]
    public IActionResult Auth([FromBody]Auth auth)
    {
      try
      {
        var userExists = _userService.GetUserByEmail(auth.Email);

        if (userExists == null)
          return BadRequest(new { Message = "Email e/ou senha está(ão) inválido(s)." });

        var token = JwtAuth.GenerateToken(userExists);

        return Ok(new
        {
          User = userExists,
          Token = token
        });
      }
      catch (Exception)
      {
        return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
      }
    }
  }
}
