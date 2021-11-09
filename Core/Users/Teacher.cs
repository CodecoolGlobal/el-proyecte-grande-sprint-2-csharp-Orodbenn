using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    class Teacher : User
    {
        public List<Subject> subjects = new List<Subject>();

        SchoolClass schoolClass { get; set; }
        public Teacher(string name, int age, string phoneNumber, string personalId, SchoolClass schoolClass) : base(name, age, phoneNumber, personalId)
        {
            this.schoolClass = schoolClass;
        }

        public void addSubject(Subject subjectToAdd)
        {
            subjects.Add(subjectToAdd);
        }
        public void removeSubject(Subject subjectToRemove)
        {
            subjects.Remove(subjectToRemove);
        }
    }
}
