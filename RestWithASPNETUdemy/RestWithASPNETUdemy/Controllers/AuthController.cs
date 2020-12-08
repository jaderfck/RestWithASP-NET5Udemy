using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Authorize("Bearer")]
	[Route("api/[controller]/v{:apiVersion}")]
	public class AuthController : ControllerBase
	{
		private ILoginBusiness _loginBusiness;

		public AuthController(ILoginBusiness loginBusiness)
		{
			_loginBusiness = loginBusiness;
		}

		[HttpPost]
		[Route("signin")]
		public IActionResult Signin([FromBody] UserVO user) 
		{
			if (user == null) return BadRequest("invlaid aklsjdlkasjdl");
			var token = _loginBusiness.ValidateCredentiasl(user);
			if (token == null) return Unauthorized();
			return Ok(token);
		}
	}
}
