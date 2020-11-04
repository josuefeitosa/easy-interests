using System;
using EasyInterests.API.Application.DTOs;
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
      public DebtController(IDebtService debtService)
      {
        _debtService = debtService;
      }

      [HttpPost]
      [Authorize( Roles = "Negotiator" )]
      public IActionResult Create([FromBody]CreateDebtDTO debt)
      {
        try
        {
          _debtService.Create(debt);

          return Ok();
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }

      }
    }
}
