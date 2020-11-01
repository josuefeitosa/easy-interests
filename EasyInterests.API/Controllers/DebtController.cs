using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
    [ApiController]
    [Route("api/debts")]
    public class DebtController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction()
        {
          return NoContent();
        }
    }
}
