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

namespace ChalkCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        //Teacher Teacher = new Teacher("Teacher Bob");
        //School school = new School(1);
        //Util util = new Util();
        //Student bob = new Student("Bob", DateTime.Parse("12 July, 2009"), "1111");
//
//
        //[HttpGet]
        //[Route("/showhomeworks")]
        //public List<Homework> showHomeworks()
        //{
        //    return Teacher.GetHomeworks();
        //}
//
        //[HttpPost]
        //[Route("/givehomework")]
        //public void addHomework([FromBody] Dictionary<string, string> response)
        //{
        //    school.addNewClasses(1);
        //    Homework homework = new Homework(school.classesInTheSchool[0], util.checkSubject(response["Subject"]), response["Description"]);
        //    Teacher.AddHomeWork(homework);
        //}
        //[HttpPost]
        //[Route("/setupexam")]
        //public void CreateExam([FromBody] Dictionary<string,string>response)
        //{
        //    school.addNewClasses(1);
        //    Teacher.AddExam(school.classesInTheSchool[0], util.checkSubject(response["Subject"]),DateTime.Parse(response["Date"]));
        //}
//
        //[HttpPost]
        //[Route("/givemark")]
        //public void AddMark([FromBody] Dictionary<string,string> response)
        //{
        //    Mark mark = new Mark(int.Parse(response["Mark"]), Teacher, util.checkSubject(response["Subject"]), util.checkMarkweight(response["MarkWeight"]));
        //    Teacher.GiveMark(mark, bob);
        //}
       




    }
}
