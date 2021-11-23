using System;
using System.Collections.Generic;
using System.Linq;
using Core.Users;

namespace Core
{
    public class School
    {
        private HashSet<Teacher> _teachersOfTheSchool = new HashSet<Teacher>();
        public List<StudentClass> _classesInTheSchool = new List<StudentClass>();

        public StudentClass GetStudentClass(string classId)
        {
            return _classesInTheSchool.FirstOrDefault(studentClass => studentClass.getClassName() == classId);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teachersOfTheSchool.Add(teacher);
        }

        public List<StudentClass> GetStudentClasses()
        {
            return _classesInTheSchool;
        }

        public void RemoveTeacher(Teacher teacher)
        {
            _teachersOfTheSchool.Remove(teacher);
        }

        public void EndOfYear()
        {
            if (_classesInTheSchool.Count <= 0) return; //TODO convert to exception
            foreach (var studentClass in _classesInTheSchool.Where(studentClass => studentClass.grade >= 12))
            {
                _classesInTheSchool.Remove(studentClass);
            }
            _classesInTheSchool.GetEnumerator().Current.yearPassing();
        }

        public void AddNewClasses(int numberOfNewClasses)
        {
            const int asciiForA = 65;
            for (var i = 0; i < numberOfNewClasses; i++)
            {
                var newClass = new StudentClass((Convert.ToChar(asciiForA + i)).ToString());
                _classesInTheSchool.Add(newClass);
            }
        }
    }
}