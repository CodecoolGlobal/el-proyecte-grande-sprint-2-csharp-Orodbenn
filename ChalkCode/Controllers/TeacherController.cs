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
    [Route("school/teacher")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TeacherController : ControllerBase
    {
        Util util = new Util();
        private readonly SchoolContext _schoolContext;

        public TeacherController(SchoolContext context)
        {
            _schoolContext = context;
        }
        
        [HttpGet]
        public async Task<List<Teacher>> getTeachers()
        {
            var Teachers = await _schoolContext.GetAllTeachers();
            return Teachers;
        }
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
        /*
        [Route("school/teacher/{id}/showhomework")]
        [HttpGet]
        public ActionResult getHomeworks(string id)
        {
            var teachers = _school.GetTeachers()
                .FirstOrDefault(t => t.ID.ToString() == id);
            if (teachers == null)
            {
                return NotFound();
            }
            var homewokrs = teachers.GetHomeworks();
            return Ok(homewokrs);
        }

       [Route ("school/teacher/{id}/addhomework")]
        public ActionResult addHomework(string id,[FromBody] Dictionary<string,string> homework)
        {
            var teachers = _school.GetTeachers()
                .FirstOrDefault(t => t.ID.ToString() == id);
            if (teachers == null)
            {
                return NotFound();
            }
            Homework freshHomework = new Homework()
            {
                Subject = util.checkSubject(homework["Subject"]),
                description = homework["Description"]
            };
            teachers.AddHomeWork(freshHomework);
            return NoContent();
        }
        */


    }
}
