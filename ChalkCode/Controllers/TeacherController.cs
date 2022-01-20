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
//using Core.DAL;
using System.Web.Http.Cors;
using Database;

namespace ChalkCode.Controllers
{
    [ApiController]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [Route("school")]
    public class TeacherController : ControllerBase
    {
        Util util = new Util();
        private readonly SchoolContext _schoolContext;

        public TeacherController(SchoolContext context)
        {
            _schoolContext = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<List<Teacher>> getTeachers()
        {
            var Teachers = await _schoolContext.GetAllTeachers();
            return Teachers;
        }



        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> getTeacher(string id)
        {
            var teachers = await _schoolContext.GetTeacherById(id);
            if (teachers == null)
            {
                return NotFound();
            }

            return Ok(teachers);
        }

        /*
         * needs:
         * {
         *    "phone": {string},
         *    "email": {string}
         * }
         */


        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {string}
         * }
         */
        [Route("{teacherId}")]
        [HttpPut]
        public async Task ChangeInfo(string teacherId, [FromBody] Dictionary<string, string> postBody)
        {
            await _schoolContext.UpdateTeacher(teacherId, postBody);
        }
        
        [Route("{id}/showhomework")]
        [HttpGet]
        public async Task<List<Homework>> getHomeworks(string id)
        {
            var homeworks = await _schoolContext.GetHomeworkForTeacher((long)int.Parse(id));
            return homeworks;
        }

        /*
         *  needs:
         * {
         *    "studentClass": {string},
         *    "subject": {string},
         *    "description": {string}
         * }
         */
        [Route("{id}/addhomework")]
        [HttpPost]
        public async Task<ActionResult> AddHomework(string id, [FromBody] Dictionary<string, string> formBody)
        {
            await _schoolContext.AddHomework(formBody, id);
            return Ok();
        }

        /*
         * needs:
         * {
         *    "studentId": {string},
         *    "teacherId": {string},
         *    "value": {string},
         *    "subject": {string},
         *    "weight": {string}
         */
        [Route("mark")]
        [HttpPost]
        public async Task AddMark([FromBody] Dictionary<string, string> formData)
        {
            await _schoolContext.AddMark(formData);
        }

        /*
         * needs:
         * {
         *    "homeworkId": {string},
         *    "desc": {string}
         */
        [Route("update-homework")]
        [HttpPut]
        public async Task UpdateHomework([FromBody] Dictionary<string, string> formBody)
        {
            await _schoolContext.UpdateHomework(formBody);
        }

        /*
         * needs:
         * {
         *    "markId": {string},
         *    "value": {string},
         *    "subject": {string},
         *    "weight": {string}
         */
        [Route("mark")]
        [HttpPut]
        public async Task UpdateMark([FromBody] Dictionary<string, string> formData)
        {
            await _schoolContext.UpdateMark(formData);
        }

        /*
         * needs:
         * {
         *    "markId": {string}
         */
        [Route("mark")]
        [HttpDelete]
        public async Task DeleteMark(string markId)
        {
            await _schoolContext.DeleteMark(markId);
        }
    }
}