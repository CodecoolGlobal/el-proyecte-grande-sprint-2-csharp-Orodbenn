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
        School school = new School(); //          placeholder
        
        [Route("/get-class/{classname}")]
        [HttpGet]
        public StudentClass GetSpecificStudentClass(string classname)
        {
            Console.WriteLine(classname);
            school.addNewClasses(3);
            Student student = new Student("asd asd", DateTime.Now, "9A");
            foreach (var studentClass in school.classesInTheSchool)
            {
                studentClass.addStudent(student);
            }
            //for testing ^^^

            foreach (var studentClass in school.classesInTheSchool)
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
            foreach (var KVPair in postBody)
            {
                school.addNewClasses(Int32.Parse(KVPair.Value));
            }
        }

        [Route("/get-student/{studentId}")]
        [HttpGet]
        public Student GetStudent(string studentId)
        {
            return FindStudent(studentId);
        }

        [Route("/get-parents-of-student/{studentId}")]
        [HttpGet]
        public HashSet<Parent> GetParentsOfAStudent(string studentId)
        {
            return FindStudent(studentId).parents;
        }

        private Student FindStudent(string studentId)
        {
            foreach (var studentClass in school.classesInTheSchool)
            {
                foreach (var student in studentClass.Students)
                {
                    if (student.PersonalId == studentId)
                    {
                        return student;
                    }
                }
            }

            return null;
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