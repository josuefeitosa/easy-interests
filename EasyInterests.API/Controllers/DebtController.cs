using System;
using System.Linq;
using System.Security.Claims;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
    [ApiController]
    [Route("api/v1/debt")]
    public class DebtController : ControllerBase
    {
      private readonly IDebtService _debtService;
      private readonly AuthenticatedUser _user;
      public DebtController(IDebtService debtService, AuthenticatedUser user)
      {
        _debtService = debtService;
        _user = user;
      }

      [HttpPost]
      [Authorize( Roles = "Negotiator" )]
      public IActionResult Create([FromBody]CreateDebtDTO debt)
      {
        try
        {
          _debtService.Create(_user.Email, debt);

          return Ok();
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }

      }

      [HttpGet]
      [AllowAnonymous]
      public IActionResult Get()
      {
        try
        {
          var debts = _debtService.GetList();

          return Ok(debts);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }

      [HttpGet]
      [Route("byuser")]
      [AllowAnonymous]
      public IActionResult GetByUser([FromQuery]int userId)
      {
        try
        {
          var debts = _debtService.GetListByUser(userId);

          return Ok(debts);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }

      [HttpPut]
      [Authorize( Roles = "Negotiator")]
      public IActionResult Update(int Id, UpdateDebtDTO updateDebt)
      {
        try
        {
          _debtService.Update(Id, updateDebt);

          return Ok("Updated");
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
    }
}
