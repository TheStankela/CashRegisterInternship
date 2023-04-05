using CashRegister.Application.Interfaces;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashRegister.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authenticationService;

		public AuthController(IAuthService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login([FromBody] UserLogin userLogin)
		{
			var user = _authenticationService.Authenticate(userLogin);
			if (user != null)
			{
				var token = _authenticationService.GenerateToken(user);
				return Ok(token);
			}
			return NotFound("User not found.");
		}
	}
}
