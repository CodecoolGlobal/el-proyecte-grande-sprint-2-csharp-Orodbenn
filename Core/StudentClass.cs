using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Users;

namespace Core
{
    public class StudentClass
    {
        public int grade { get; set; }
        public string classIdentifier { get; } // 9a vs 9b and so on
        private List<Student> students = new List<Student>();
        private List<Teacher> teachersOfTheClass = new List<Teacher>();
        public int classRoom { get; set; } // can be changed if letters are involved as well

        public StudentClass(string classIdentifier)
        {
            this.classIdentifier = classIdentifier;
            grade = 9; // based on regular high schools
        }
        public StudentClass()
        {

        }

        public void yearPassing()
        {
            grade++;
        }

        public Student GetStudent(string studentId)
        {
            foreach (var student in students)
            {
                if (student.PersonalId == studentId)
                {
                    return student;
                }
            }

            return null;
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
            return grade + classIdentifier;
        }
    }
}
