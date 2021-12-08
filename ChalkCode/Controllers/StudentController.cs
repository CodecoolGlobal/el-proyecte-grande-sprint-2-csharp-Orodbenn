using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Core;
using Core.Marks;
using Core.Users;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController, Route("/Students/{studentId}")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class StudentController : Controller
    {
        
        private SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<Student> GetStudent(string studentId)
        {
            return await Task<Student>.Run(() => _context.GetStudent(studentId).Result);
        }
        
        /*
         * needs:
         * {
         *    "oldClassId": {string},
         *    "newClassId": {string}
         * }
         */
        [Route("change-class")]
        [HttpPost]
        public async Task ChangeClassOfStudent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.ChangeClassOfStudent(studentId, postBody);
        }

        [Route("parents")]
        [HttpGet]
        public async Task<HashSet<Parent>> GetParentsOfAStudent(string studentId)
        {
            return await Task<HashSet<Parent>>.Run(() => _context.GetParents(studentId).Result);
        }

        [Route("marks")]
        [HttpGet]
        public async Task<List<Mark>> GetMarksOfAStudent(string studentId)
        {
            return await Task<List<Mark>>.Run(() => _context.GetMarks(studentId).Result);
        }

        [Route("homework")]
        [HttpGet]
        public async Task<List<Homework>> GetHomeworkOfAStudent(string studentId)
        {
            return await Task<List<Homework>>.Run(() => _context.GetHomework(studentId).Result);
        }

        /*
         * needs:
         * {
         *    "phone": {string}
         *    "email": {string}
         * }
         */
        [Route("contacts")]
        [HttpPost]
        public async Task AddContactInformation(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            if (postBody.Count == 1)
            {
                if (postBody.ContainsKey("email"))
                {
                    await _context.SaveEmail(studentId, postBody);
                }
                else
                {
                    await _context.SavePhoneNumber(studentId, postBody);
                }
            }
            else
            {
                await _context.SaveEmail(studentId, postBody);
                await _context.SavePhoneNumber(studentId, postBody);
            }
        }

        /*
         * needs:
         * {
         *    "name": {string}
         * }
         */
        [Route("parents")]
        [HttpPost]
        public async Task AddParent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.AddParentOfStudent(studentId, postBody);
        }

        /*
         * needs:
         * {
         *    "phone": {string},
         *    "email": {string}
         * }
         */
        [Route("parents/{parentId}")]
        [HttpPost]
        public async Task AddParentContact(string parentId, [FromBody] Dictionary<string, string> postBody)
        {
            
            if (postBody.Count == 1)
            {
                if (postBody.ContainsKey("email"))
                {
                    await _context.SaveEmailParent(parentId, postBody);
                }
                else
                {
                    await _context.SavePhoneNumberParent(parentId, postBody);
                }
            }
            else
            {
                await _context.SaveEmailParent(parentId, postBody);
                await _context.SavePhoneNumberParent(parentId, postBody);
            }
        }

        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {string}
         * }
         */
        [Route("parents/{parentid}")]
        [HttpPut]
        public async Task ChangeInfoParent(string parentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.UpdateStudentParent(parentId, postBody);
        }

        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {string}
         * }
         */
        [Route("")]
        [HttpPut]
        public async Task ChangeInfo(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            await _context.UpdateStudent(studentId, postBody);
        }
    }
}