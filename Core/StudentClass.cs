using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Users;

namespace Core
{
    public class StudentClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public int grade { get; set; }
        public string classIdentifier { get; set;  } // 9a vs 9b and so on
        public int classRoom { get; set; } // can be changed if letters are involved as well

        public List<Student> Students { get; set;  } = new List<Student>();
        private List<Teacher> teachersOfTheClass = new List<Teacher>();

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
            foreach (var student in Students)
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
            Students.Add(student);
        }

        public void removeStudent(Student studentToRemove)
        {
            Students.Remove(studentToRemove);
        }

        public void addTeacher(Teacher teacher)
        {
            teachersOfTheClass.Add(teacher);
        }

        public void removeTeacher(Teacher teacherToRemove)
        {
            teachersOfTheClass.Remove(teacherToRemove);
        }


        public List<Teacher> TeachersOfTheClass => teachersOfTheClass;

        public string getClassName()
        {
            return grade + classIdentifier.ToLower().Trim();
        }
    }
}