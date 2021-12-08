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
        public int classRoom { get; set; } // can be changed if letters are involved as well
        public string classIdentifier { get; set;  } // 9a vs 9b and so on

        public List<Homework> Homework { get; set;  } = new List<Homework>();
        public List<Student> Students { get; set;  } = new List<Student>();
        public List<Teacher> TeachersOfTheClass { get; set;  } = new List<Teacher>();

        public StudentClass(int grade)
        {
            this.grade = grade;
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
            TeachersOfTheClass.Add(teacher);
        }

        public void removeTeacher(Teacher teacherToRemove)
        {
            TeachersOfTheClass.Remove(teacherToRemove);
        }

        public string getClassName()
        {
            return grade + classIdentifier.ToLower().Trim();
        }
    }
}