using System;
using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
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
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
      return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
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
    public ActionResult<User> GetUserByEmail([FromQuery]string email)
    {
      try
      {
        return Ok(_userService.GetUserByEmail(email));
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

    [HttpPost]
    public IActionResult Post([FromBody]CreateUserDTO user)
    {
      try
      {
        _userService.Create(user);

        return Created("User created", user);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    public IActionResult Put([FromQuery]int Id,[FromBody]UpdateUserDTO user)
    {
      try
      {
        _userService.Update(Id, user);

        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
