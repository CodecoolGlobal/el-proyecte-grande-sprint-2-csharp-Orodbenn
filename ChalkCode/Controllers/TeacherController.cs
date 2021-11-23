using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Users;
using Core;
using Core.Utils;
using Core.Marks;
using Core.DAL;

namespace ChalkCode.Controllers
{
    
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private School _school;

        public TeacherController(IRepository<School> repository)
        {
            _school = repository.GetSchool();
        }
        [Route("school/teachers")]
        [HttpGet]
        public ActionResult getTeachers()
        {
            return Ok(_school.GetTeachers());
        }





    }
}
