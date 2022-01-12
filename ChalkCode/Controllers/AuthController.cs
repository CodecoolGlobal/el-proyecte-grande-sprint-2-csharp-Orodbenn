using Core.Users;
using Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ChalkCode.Controllers
{
    [ApiController,Route("/login")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AuthController : Controller
    {
        private readonly SchoolContext _context;

        public AuthController(SchoolContext context)
        {
            _context = context;
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
                var claims = new List<Claim>();
                claims.Add(new Claim("Username", Credentials["Username"]));
                claims.Add(new Claim("Password", Credentials["Password"]));
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
                return Ok(auth.Role);
            }
            
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
