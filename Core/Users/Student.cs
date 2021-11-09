using Core.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Student : User
    {
        public List<Mark> marks { get; set; }
        public HashSet<Parent> parents { get; set; }
        public Student(string name, int age, string phoneNumber, string personalId) : base(name, age, phoneNumber, personalId)
        {
            marks = new List<Mark>();
            parents = new HashSet<Parent>();
        }
    }
}
