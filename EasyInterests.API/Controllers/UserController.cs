using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction()
        {
          return NoContent();
        }
    }
}
