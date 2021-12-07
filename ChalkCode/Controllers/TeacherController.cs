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

namespace ChalkCode.Controllers
{
    
    [ApiController]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TeacherController : ControllerBase
    {
        Util util = new Util();
        private School _school;

        public TeacherController(IRepository<School> repository)
        {
            _school = repository.GetSchool();
        }
        [Route("school/teacher")]
        [HttpGet]
        public ActionResult getTeachers()
        {
            return Ok(_school.GetTeachers());
        }

        [Route("school/teacher/{id}")]
        [HttpGet]
        public ActionResult getTeacher(string id)
        {
            var teachers = _school.GetTeachers()
                .FirstOrDefault(t => t.ID.ToString() == id);
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);

        }

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
       

    }
}
