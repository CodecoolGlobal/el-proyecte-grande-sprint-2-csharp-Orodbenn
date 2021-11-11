using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
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
            dummy.addNewClasses(3);
            Student student = new Student("asd asd", DateTime.Now, "9A");
            foreach (var studentClass in dummy.classesInTheSchool)
            {
                studentClass.addStudent(student);
            }
            //for testing ^^^

            foreach (var studentClass in dummy.classesInTheSchool)
            {
                if (studentClass.getClassName() == classname)
                {
                    return studentClass;
                }
            }

            return null;
        }

        [Route("/add-new-classes")]
        [HttpPost]
        public void AddNewClasses([FromBody]Dictionary<string, string> postBody)
        {
            School school = new School(); // placeholder
            foreach (var KVPair in postBody)
            {
                school.addNewClasses(Int32.Parse(KVPair.Value));
            }
        }

        /* unfinished code, might be useful
         
        [Route("/every-class")]
        [HttpGet]
        public HashSet<StudentClass> GetClasses(School school)
        {
            return school.classesInTheSchool;
        }

        [Route("/classes")]
        [HttpPost]
        public void AddClasses([FromBody] JsonContent body)
        {
            
            // school.addNewClasses(int, int)
        }*/
    }
}