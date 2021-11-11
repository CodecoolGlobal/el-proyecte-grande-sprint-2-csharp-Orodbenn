using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Users;
using Core;
using Core.Utils;

namespace ChalkCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        Teacher Teacher = new Teacher("Teacher Bob");
        School school = new School();
        Util util = new Util();



        [HttpGet]
        [Route("/showhomeworks")]
        public List<Homework> showHomeworks()
        {
            return Teacher.GetHomeworks();
        }

        [HttpPost]
        [Route("/givehomework")]
        public void addHomework([FromBody] Dictionary<string, string> response)
        {
            school.addNewClasses(1, 101);
            Homework homework = new Homework(school.classesInTheSchool[0], util.checkSubject(response["Subject"]), response["Description"]);
            Teacher.AddHomeWork(homework);
        }
        [HttpPost]
        [Route("/setupexam")]
        public void CreateExam([FromBody] Dictionary<string,string>response)
        {
            school.addNewClasses(1, 101);
            var jsndate = DateTime.Parse(response["Date"]);
            Teacher.AddExam(school.classesInTheSchool[0], util.checkSubject(response["Subject"]), jsndate);
        }




    }
}
