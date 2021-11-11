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
        School school = new School(1); //          placeholder

        [Route("/class/{studentClass}/get-room")]
        [HttpGet]
        public int GetClassRoom(string studentClass)
        {
            return school.GetStudentClass(studentClass).classRoom;
        }

        [Route("/class/{studentClass}/change-room")]
        [HttpPost]
        public void ChangeClassRoom(string studentClass, [FromBody] Dictionary<string, string> postBody)
        {
            school.GetStudentClass(studentClass).classRoom = Int32.Parse(postBody["roomNumber"]);
        }

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

        /*
         * needs:
         * {
         *    "name": {string},
         *    "birthDate": {Date}
         * }
         */
        [Route("/add-new-student/{className}")]
        [HttpPost]
        public void AddNewStudent(string className, [FromBody] Dictionary<string, string> postBody)
        {
            Student student = new Student(postBody["name"], DateTime.Parse(postBody["birthDate"]), className);
            school.GetStudentClass(className).addStudent(student);
        }

        /*
         * needs:
         * {
         *    "studentId": {string},
         *    "oldClassId": {string},
         *    "newClassId": {string}
         * }
         */
        [Route("/student/change-class")]
        [HttpPost]
        public void ChangeClassOfStudent([FromBody] Dictionary<string, string> postBody)
        {
            StudentClass oldClass = school.GetStudentClass(postBody["oldClassId"]);
            Student student = oldClass.GetStudent(postBody["studentId"]);
            school.GetStudentClass(postBody["newClassId"]).addStudent(student);
            oldClass.removeStudent(student);
        }

        /* 
         * needs:
         * { "numberOfClasses": {int} }
         */
        [Route("/add-new-classes")]
        [HttpPost]
        public void AddNewClasses([FromBody] Dictionary<string, string> postBody)
        {
            school.addNewClasses(Int32.Parse(postBody["numberOfClasses"]));
        }

        [Route("/end-of-year")]
        [HttpPost]
        public void EndOfYear()
        {
            school.endOfYear();
        }

        /*
         * needs:
         * {
         *    "name": {string}
         * }
         */
        [Route("/student/{studentId}/add-parent")]
        [HttpPost]
        public void AddParent(string studentId, [FromBody] Dictionary<string, string> postBody)
        {
            Student student = FindStudent(studentId);
            Parent parent = new Parent(postBody["name"], student);
            student.AddParent(parent);
        }

        [Route("/student/{studentId}")]
        [HttpGet]
        public Student GetStudent(string studentId)
        {
            return FindStudent(studentId);
        }

        [Route("/student/{studentId}/parents")]
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