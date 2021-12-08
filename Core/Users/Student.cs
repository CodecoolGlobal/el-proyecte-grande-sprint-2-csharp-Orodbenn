using Core.Marks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Student : User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public List<Mark> marks { get; set; } = new List<Mark>();
        public HashSet<Parent> parents { get; set; } = new HashSet<Parent>();
        //public Homework homeworks { get; }
        public Dictionary<Subject, DateTime> exams;

        public Student(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
        }

        public Student()
        {
        }

        public void AssignId(string classId)
        {
            PersonalId = "S" + DateTime.Now.Year.ToString()[2..] + classId[^1].ToString().ToUpper() + name.Substring(0, 1).ToUpper() + 
                         name.Substring(name.IndexOf(" ") + 1, 2).ToUpper();
        }

        public void AddParent(Parent parent)
        {
            parents.Add(parent);
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
