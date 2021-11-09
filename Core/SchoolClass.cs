﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Users;

namespace Core
{
    class SchoolClass
    {
        private int grade;
        private string classIdentifier; // 9a vs 9b and so on
        private List<Student> students = new List<Student>();
        private List<Teacher> teachersOfTheClass = new List<Teacher>();
        private int classRoom { get; set; } // can be changed if letters are involved as well

        public SchoolClass(string classIdentifier, int classRoom)
        {
            this.classIdentifier = classIdentifier.ToLower();
            this.classRoom = classRoom;
            grade = 9;
        }

        public void yearPassing()
        {
            grade++;
        }

        public void addStudent(Student student)
        {
            students.Add(student);
        }

        public void removeStudent(Student studentToRemove)
        {
            students.Remove(studentToRemove);
        }

        public void addTeacher(Teacher teacher)
        {
            teachersOfTheClass.Add(teacher);
        }

        public void removeTeacher(Teacher teacherToRemove)
        {
            teachersOfTheClass.Remove(teacherToRemove);
        }

        public List<Student> Students => students;

        public List<Teacher> TeachersOfTheClass => teachersOfTheClass;

        public string getClassName()
        {
            return grade + "/" + classIdentifier;
        }
    }
}
