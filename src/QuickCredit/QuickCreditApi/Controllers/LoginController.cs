using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCreditApi.Models.Entities;
using QuickCreditApi.Models.ViewModels;
using QuickCreditApi.Services;

namespace QuickCreditApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly qcdbContext _context;
        public LoginController(ILoginService loginService, qcdbContext context)
        {
            _loginService = loginService;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns>A token valid for 7 days</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultApiConventions), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Authenticate([FromBody]UserTokenViewModel userParam)
        {
            var user = _loginService.Authenticate(userParam.Account, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        

       
    }
}
