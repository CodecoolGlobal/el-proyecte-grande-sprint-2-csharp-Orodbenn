using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core;
using Core.DAL;
using Core.Users;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController, Route("/class")]
    public class SchoolController : Controller
    {
        private SchoolContext _context;

        public SchoolController(SchoolContext context)
        {
            _context = context;
        }

        [Route("list-classes")]
        [HttpGet]
        public List<StudentClass> GetClasses()
        {
            return _context.GetStudentClasses().Result;
        }

        [Route("{studentClass}/room")]
        [HttpGet]
        public int GetClassRoom(string studentClass)
        {
            return _context.GetStudentClass(studentClass).Result.classRoom;
        }

        /*
         * needs:
         * {
         *    "roomNumber": {int}
         * }
         */
        [Route("{studentClass}/room")]
        [HttpPut]
        public void ChangeClassRoom(string studentClass, [FromBody] Dictionary<string, int> postBody)
        {
            _context.ChangeClassRoom(studentClass, postBody["roomNumber"]);
        }

        [Route("{classname}")]
        [HttpGet]
        public StudentClass GetSpecificStudentClass(string studentClass)
        {
            return _context.GetStudentClass(studentClass).Result;
        }

        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {Date}
         * }
         */
        [Route("{className}/add-new-student")]
        [HttpPost]
        public void AddNewStudent(string className, [FromBody] Dictionary<string, string> postBody)
        {
            Student student = new Student(postBody["name"], DateTime.Parse(postBody["birthDate"]), className);
            _context.AddNewStudentToClass(className, student);
        }

        /* 
         * needs:
         * { "numberOfClasses": {int} }
         */
        [Route("add-new-classes")]
        [HttpPost]
        public async void AddNewClasses([FromBody] Dictionary<string, int> postBody)
        {
            await _context.AddNewClasses(postBody["numberOfClasses"]);
        }

        [Route("end-of-year")]
        [HttpPost]
        public void EndOfYear()
        {
            _context.EndOfYear();
        }
    }
}