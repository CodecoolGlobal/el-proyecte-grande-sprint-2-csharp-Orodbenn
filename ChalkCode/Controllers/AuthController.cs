using Core.Users;
using Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task AuthUser([FromBody] Dictionary<string,string>Credentials)
        {
            await _context.GetTeacher(Credentials["Username"],Credentials["Password"]);
            
        }
    }
}
