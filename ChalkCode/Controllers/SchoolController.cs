using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Core;
using Core.DAL;
using Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController]
    public class SchoolController : Controller
    {
        private School _school;

        public SchoolController(IRepository<School> repository)
        {
            _school = repository.GetSchool();
        }

        [Route("/class/{studentClass}/get-room")]
        [HttpGet]
        public int GetClassRoom(string studentClass)
        {
            return _school.GetStudentClass(studentClass).classRoom;
        }

        /*
         * needs:
         * {
         *    "roomNumber": {int}
         * }
         */
        [Route("/class/{studentClass}/change-room")]
        [HttpPost]
        public void ChangeClassRoom(string studentClass, [FromBody] Dictionary<string, int> postBody)
        {
            _school.GetStudentClass(studentClass).classRoom = postBody["roomNumber"];
        }

        [Route("/get-class/{classname}")]
        [HttpGet]
        public StudentClass GetSpecificStudentClass(string classname)
        {
            return _school.GetStudentClasses().FirstOrDefault(studentClass => studentClass.getClassName() == classname);
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
            _school.GetStudentClass(className).addStudent(student);
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
            StudentClass oldClass = _school.GetStudentClass(postBody["oldClassId"]);
            Student student = oldClass.GetStudent(postBody["studentId"]);
            _school.GetStudentClass(postBody["newClassId"]).addStudent(student);
            oldClass.removeStudent(student);
        }

        /* 
         * needs:
         * { "numberOfClasses": {int} }
         */
        [Route("/add-new-classes")]
        [HttpPost]
        public void AddNewClasses([FromBody] Dictionary<string, int> postBody)
        {
            _school.AddNewClasses(postBody["numberOfClasses"]);
        }

        [Route("/end-of-year")]
        [HttpPost]
        public void EndOfYear()
        {
            _school.EndOfYear();
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
            foreach (var studentClass in _school.GetStudentClasses())
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

        [Route("/list-classes")]
        [HttpGet]
        public List<StudentClass> GetClasses()
        {
            return _school.GetStudentClasses();
        }
    }
}