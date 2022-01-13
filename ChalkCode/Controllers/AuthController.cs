using Core.Users;
using Database;
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
                var token = GenerateToken(auth);
                return new ObjectResult(token);
            }
            
        }
        private async Task<dynamic> GenerateToken(Teacher auth)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, auth.name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, auth.name));
            claims.Add(new Claim(ClaimTypes.Role, auth.Role));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToString()));
            

            
            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials
                    (new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ultimate_top_secret_key_dont_tell_them")), //For testing fine asis but this needs to be secure
                    SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims)
                );
            var output = new
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = auth.name
            };

            return output;
        }
        [AuthorizeWithToken]
        [HttpGet]
        [Route("auth")]
        public async Task<IActionResult> Logout()
        {
            
            return Ok();
        }
    }
}
