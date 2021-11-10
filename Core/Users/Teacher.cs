using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Teacher : User
    {
        public List<Subject> subjects = new List<Subject>();
        
        public Teacher(string name) : base(name)
        {
            AssignId();
        }

        public void addSubject(Subject subjectToAdd)
        {
            subjects.Add(subjectToAdd);
        }
        public void removeSubject(Subject subjectToRemove)
        {
            subjects.Remove(subjectToRemove);
        }

        private void AssignId()
        {
            PersonalId = "T" + name.Substring(0, 1).ToUpper() +
                         name.Substring(name.IndexOf(" "), 2).ToUpper();
        }
    }
}
