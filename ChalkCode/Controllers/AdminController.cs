using Core.Marks;
using Core.Users;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ChalkCode.Controllers
{
    [ApiController]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [Route("admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SchoolContext _context;

        public AdminController(SchoolContext context)
        {
            _context = context;
        }
        [Route("addteacher")]
        [HttpPost]
        public async Task<ActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            await _context.AddTeacher(teacher);
            return Ok();
        }

        [Route("{className}/add-new-student")]
        [HttpPost]
        public async Task AddNewStudent(string className, [FromBody] Dictionary<string, string> postBody)
        {
            var student = new Student(postBody["name"], DateTime.Parse(postBody["birthDate"]));
            await _context.AddNewStudent(className, student);
        }

        [Route("delete-user")]
        [HttpDelete]
        public async Task DeleteUser(string userId)
        {
            await _context.DeleteUser(userId);
        }

        [Route("add-new-classes")]
        [HttpPost]
        public async Task AddNewClasses([FromBody] Dictionary<string, int> postBody)
        {
            await _context.AddNewClasses(postBody["numberOfClasses"]);
        }

        [Route("end-of-year")]
        [HttpPost]
        public async Task EndOfYear()
        {
            await _context.EndOfYear();
        }

        [Route("change-class")]
        [HttpPost]
        public async Task ChangeClassOfStudent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.ChangeClassOfStudent(studentId, postBody);
        }

        [Route("{teacherId}/add-contact")]
        [HttpPost]
        public async Task AddContacts(string teacherId, [FromBody] Dictionary<string, string> postBody)
        {
            if (postBody.Count == 1)
            {
                if (postBody.ContainsKey("email"))
                {
                    await _context.SaveEmailTeacher(teacherId, postBody);
                }
                else
                {
                    await _context.SavePhoneNumberTeacher(teacherId, postBody);
                }
            }
            else
            {
                await _context.SaveEmailTeacher(teacherId, postBody);
                await _context.SavePhoneNumberTeacher(teacherId, postBody);
            }
        }
    }
}

