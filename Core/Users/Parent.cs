using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Parent : User
    {
        public HashSet<Student> students = new HashSet<Student>();

        public Parent(string name, Student student) : base(name)
        {
            students.Add(student);
            student.parents.Add(this);
            AssignId(student);
        }
        
        public void addStudent(Student studentToAdd)
        {
            students.Add(studentToAdd);
        }

        private void AssignId(Student student)
        {
            string id = student.PersonalId;
            id = id.Substring(0, 1).Replace("S", "P");
            PersonalId = id;
        }
    }
}
