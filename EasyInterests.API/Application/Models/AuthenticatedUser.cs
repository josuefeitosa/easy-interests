using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EasyInterests.API.Application.Models
{
    public class AuthenticatedUser
    {
			private readonly IHttpContextAccessor _accessor;

			public AuthenticatedUser(IHttpContextAccessor accessor)
			{
				_accessor = accessor;
			}

			public string Email => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
			public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
			public string Role => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;

			public IEnumerable<Claim> GetClaimsIdentity()
			{
				return _accessor.HttpContext.User.Claims;
			}
		}
}
