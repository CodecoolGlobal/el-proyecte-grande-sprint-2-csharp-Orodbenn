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
        public Student(string name, DateTime dateOfBirth, string classId) : base(name, dateOfBirth)
        {
            marks = new List<Mark>();
            parents = new HashSet<Parent>();
            AssignId(classId);
        }

        private void AssignId(string classId)
        {
            PersonalId = "S" + DateTime.Now.Year + classId + name.Substring(0, 1).ToUpper() + 
                         name.Substring(name.IndexOf(" "), 2).ToUpper();
        }
    }
}
