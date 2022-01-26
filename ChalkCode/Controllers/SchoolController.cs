using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Core;
//using Core.DAL;
using Core.Users;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController]
    [EnableCors(origins: "https://orange-pond-00dd1b803.1.azurestaticapps.net", headers: "*", methods: "*")]
    public class SchoolController : Controller
    {
        private readonly SchoolContext _context;

        public SchoolController(SchoolContext context)
        {
            _context = context;
        }

        [Route("list-classes")]
        [HttpGet]
        public async Task<List<StudentClass>> GetClasses()
        {
            return await Task<List<StudentClass>>.Run(() => _context.GetStudentClasses().Result);
        }

        [Route("{studentClass}/room")]
        [HttpGet]
        public async Task<int> GetClassRoom(string studentClass)
        {
            return await Task<int>.Run(() => _context.GetStudentClass(studentClass).Result.classRoom);
        }

        /*
         * needs:
         * {
         *    "roomNumber": {int}
         * }
         */
        [Route("{studentClass}/room")]
        [HttpPost]
        public async Task ChangeClassRoom(string studentClass, [FromBody] Dictionary<string, int> postBody)
        {
            await _context.ChangeClassRoom(studentClass, postBody["roomNumber"]);
        }

        [Route("{classname}")]
        [HttpGet]
        public async Task<StudentClass> GetSpecificStudentClass(string classname)
        {
            return await Task.Run(() => _context.GetStudentClass(classname).Result);
        }

        [Route("{classname}/students")]
        [HttpGet]
        public async Task<List<Student>> GetAllStudentsOfClass(string classname)
        {
            return await Task.Run(() => _context.GetStudentClass(classname).Result.Students);
        }

        [Route("setparents")]
        [HttpPost]
        public async Task AddParent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.AddParentOfStudent(studentId, postBody);
        }

        [Route("parents/{parentid}")]
        [HttpPut]
        public async Task ChangeInfoParent(string parentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.UpdateStudentParent(parentId, postBody);
        }

        //[Route("{classname}/teachers")]
        //[HttpGet]
        //public async Task<List<Teacher>> GetAllTeachersOfClass(string classname)
        //{
        //    return await Task.Run(() => _context.GetStudentClass(classname).Result);
        //}

        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {string}
         * }
         */


        /* 
         * needs:
         * { "numberOfClasses": {int} }
         */

    }
}