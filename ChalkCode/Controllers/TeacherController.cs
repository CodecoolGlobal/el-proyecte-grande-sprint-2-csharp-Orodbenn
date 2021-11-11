using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Users;
using Core;

namespace ChalkCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        Teacher Teacher = new Teacher("Teacher Bob");

        /*
        [HttpGet]
        [Route("/showHomeworks")]
        public Dictionary<StudentClass, Dictionary<DateTime, string>> showHomeworks()
        {
            return Teacher.getHomeworks();
        }

        */

    }
}
