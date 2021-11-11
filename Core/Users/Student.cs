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
        public Dictionary<Subject, Dictionary<DateTime, string>> homeworks { get; }
        public Dictionary<Subject, DateTime> exams;

        public Student(string name, DateTime dateOfBirth, string classId) : base(name, dateOfBirth)
        {
            marks = new List<Mark>();
            parents = new HashSet<Parent>();
           // AssignId(classId);
        }

        private void AssignId(string classId)
        {
            PersonalId = "S" + DateTime.Now.Year + classId[^1] + name.Substring(0, 1).ToUpper() + 
                         name.Substring(name.IndexOf(" ") + 1, 2).ToUpper();
        }

        public void AddParent(Parent parent)
        {
            parents.Add(parent);
        }

        public void AddHomeWork(Subject subject, Dictionary<DateTime, string> homework)
        {
            homeworks.Add(subject, homework);
        }

        public void AddExam(Subject subject, DateTime date)
        {
            exams.Add(subject, date);
        }

        public void AddMark(Mark mark)
        {
            marks.Add(mark);
        }
    }
}
