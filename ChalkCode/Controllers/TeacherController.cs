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
using System.Web.Http.Cors;
using Database;

namespace ChalkCode.Controllers
{
    
    [ApiController]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TeacherController : ControllerBase
    {
        Util util = new Util();
        private readonly SchoolContext _schoolContext;

        public TeacherController(SchoolContext context)
        {
            _schoolContext = context;
        }
        [Route("school/teacher")]
        [HttpGet]
        public async Task<List<Teacher>> getTeachers()
        {
            var Teachers = await _schoolContext.GetAllTeachers();
            return Teachers;
        }
        [Route("school/addteacher")]
        [HttpPost]
        public async Task<ActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            await _schoolContext.AddTeacher(teacher);
            return Ok();
        }

        
        [Route("school/teacher/{id}")]
        [HttpGet]
        public async Task<ActionResult> getTeacher(int id)
        {
            var teachers = await _schoolContext.GetTeacherById(id);
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);

        }
        
        [Route("school/teacher/{id}/showhomework")]
        [HttpGet]
        public async Task<List<Homework>> getHomeworks(int id)
        {
            var homeworks = await _schoolContext.GetHomeworkForTeacher(id);
            return homeworks;
        }
        
       [Route ("school/teacher/{id}/addhomework")]
       [HttpPost]
        public async Task<ActionResult> addHomework(int id,[FromBody] Homework homework)
        {
            await _schoolContext.AddHomework(homework, id);
            return Ok();
        }
        


    }
}
