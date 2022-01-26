using ChalkCode.Auth;
using Core.Users;
using Database;
using JWTWebAuthentication.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ChalkCode.Controllers
{
    [ApiController,Route("/login")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AuthController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IJWTManagerRepository _jWTManager;

        public AuthController(SchoolContext context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            this._jWTManager = jWTManager;
        }

        [Route("auth")]
        [HttpPost]
        public async Task<IActionResult> AuthUser([FromBody] Dictionary<string,string>Credentials)
        {
           var auth = await _context.GetTeacher(Credentials["Username"],Credentials["Password"]);
            if(auth == null)
            {
                return BadRequest();
            }
            else
            {
                var token = await _jWTManager.Authenticate(auth);
                return Ok(token);
            }
            
        }
        
        
        [HttpGet]
        [Route("auth")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            
            return Ok();
        }
    }
}
