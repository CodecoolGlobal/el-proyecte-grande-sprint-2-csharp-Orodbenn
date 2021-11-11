using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Core;
using Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController]
    public class SchoolController
    {
        [Route("/get-class/{classname}")]
        [HttpGet]
        public StudentClass GetSpecificStudentClass(string classname)
        {
            Console.WriteLine(classname);
            School dummy = new School();
            dummy.addNewClasses(3, 10);
            Student student = new Student("asd asd", DateTime.Now, "9A");
            foreach (var studentClass in dummy.classesInTheSchool)
            {
                studentClass.addStudent(student);
                Console.WriteLine(studentClass.getClassName());
            }
            
            foreach (var studentClass in dummy.classesInTheSchool)//school.classesInTheSchool)
            {
                Console.WriteLine(studentClass.getClassName() == classname);
                if (studentClass.getClassName() == classname)
                {
                    return studentClass;
                }
            }

            return null;
        }
        
        //[Route("/every-class")]
        //[HttpGet]
        //public HashSet<StudentClass> GetClasses(School school)
        //{
        //    return school.classesInTheSchool;
        //}

        //[Route("/classes")]
        //[HttpPost]
        //public void AddClasses([FromBody] JsonContent body)
        //{
        //    
        //    // school.addNewClasses(int, int)
        //}
    }
}