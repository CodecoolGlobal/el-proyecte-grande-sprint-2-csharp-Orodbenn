﻿using System;
using System.Collections.Generic;
using Core.Users;

namespace Core
{
    public class School
    {
        private HashSet<Teacher> teachersOfTheSchool = new HashSet<Teacher>();
        public List<StudentClass> classesInTheSchool = new List<StudentClass>();
        public School school { get; }
        public School()
        {
            
        }
        
        /*private IRepository<Room> Repository { get; }

        public RoomService(IRepository<Room> repository)
        {
            Repository = repository;
        }*/

        public StudentClass GetStudentClass(string classId)
        {
            foreach (var studentClass in classesInTheSchool)
            {
                if (studentClass.getClassName() == classId)
                {
                    return studentClass;
                }
            }

            return null;
        }

        public void addTeacher(Teacher teacher)
        {
            teachersOfTheSchool.Add(teacher);
        }

        public List<StudentClass> getStudentClass()
        {
            return classesInTheSchool;
        }
        
        public void removeTeacher(Teacher teacher)
        {
            teachersOfTheSchool.Remove(teacher);
        }

        public void endOfYear()
        {
            //classesInTheSchool.RemoveWhere(SchoolClass => SchoolClass.grade <= 12);

            classesInTheSchool.GetEnumerator().Current.yearPassing();
        }

        public void addNewClasses(int numberOfNewClasses)
        {
            int AsciiForA = 65;
            for(int i = 0; i < numberOfNewClasses; i++)
            {
                StudentClass newClass = new StudentClass((Convert.ToChar(AsciiForA + i)).ToString());
                classesInTheSchool.Add(newClass);
            }
        }
    }
}