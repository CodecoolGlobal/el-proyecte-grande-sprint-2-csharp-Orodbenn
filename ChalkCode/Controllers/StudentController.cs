using System.Collections.Generic;
using Core;
using Core.Users;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController, Route("/students")]
    public class StudentController : Controller
    {
        
        private SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        [Route("{studentId}")]
        [HttpGet]
        public Student GetStudent(string studentId)
        {
            return _context.GetStudent(studentId).Result;
        }
        
        /*
         * needs:
         * {
         *    "oldClassId": {string},
         *    "newClassId": {string}
         * }
         */
        [Route("{studentId}/change-class")]
        [HttpPost]
        public void ChangeClassOfStudent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            _context.ChangeClassOfStudent(studentId, postBody);
        }

        [Route("{studentId}/parents")]
        [HttpGet]
        public HashSet<Parent> GetParentsOfAStudent(string studentId)
        {
            return _context.GetParents(studentId).Result;
        }

        /*
         * needs:
         * {
         *    "name": {string}
         * }
         */
        [Route("{studentId}/add-parent")]
        [HttpPost]
        public void AddParent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            _context.AddParentOfStudent(studentId, postBody);
        }
    }
}