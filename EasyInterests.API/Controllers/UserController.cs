using System;
using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.Services;
using EasyInterests.API.Application.ViewModels;
using EasyInterests.API.Enums;
using EasyInterests.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
  [ApiController]
  [Route("api/v1/users")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly AuthenticatedUser _user;

    public UserController(IUserService userService, AuthenticatedUser user)
    {
      _userService = userService;
      _user = user;
    }

    [HttpGet]
    [Authorize(Roles = "Negotiator")]
    public ActionResult<List<UserListViewModel>> GetAll()
    {
      return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Negotiator")]
    public ActionResult<UserListViewModel> GetUser(int id)
    {
      try
      {
        return Ok(_userService.GetUser(id));
      }
      catch (Exception e)
      {
        if (e.Message.Contains("Not Found"))
        {
          return NotFound();
        }
        else
        {
          return BadRequest(e.Message);
        }
      }
    }

    [HttpGet]
    [Route("email")]
    [Authorize(Roles = "Negotiator")]
    public ActionResult<User> GetUserByEmail([FromQuery]string email)
    {
      var user = _userService.GetUserByEmail(email);

      if (user is null)
        return NotFound("User not found");

      return user;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody]CreateUserDTO user)
    {
      try
      {
        var existingUser = _userService.GetUserByEmail(user.Email);

        if (existingUser != null)
          return Conflict("This email is already in use.");

        _userService.Create(user);

        return Created("User created", user);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Authorize(Roles = "Customer,Negotiator")]
    public IActionResult Put([FromQuery]int Id,[FromBody]UpdateUserDTO user)
    {
      try
      {
        var existingUser = _userService.GetUser(Id);

        if (existingUser is null)
          return NotFound("User does not exists.");

        if (_user.Email == existingUser.Email || _user.Role == UserRolesEnum.Negotiator.ToString())
        {
          _userService.Update(Id, user);
          return Ok("Updated");
        }
        else
        {
          return Unauthorized("You can not update this user.");
        }

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
