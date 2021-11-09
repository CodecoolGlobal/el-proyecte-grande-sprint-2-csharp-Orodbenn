using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    class Parent : User
    {
        public HashSet<Student> students = new HashSet<Student>();

        public Parent(string name, int age, string phoneNumber, string personalId) : base(name, age, phoneNumber, personalId)
        {
            
        }
        
        public void addStudent(Student studentToAdd)
        {
            students.Add(studentToAdd);
        }

        public void removeStudent(Student studentToRemove)
        {
            students.Remove(studentToRemove);
        }


    }
}
