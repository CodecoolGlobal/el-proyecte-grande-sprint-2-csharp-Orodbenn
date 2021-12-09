using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Marks;

namespace Core.Users
{
    public class Teacher : User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public List<Subject> subjects = new List<Subject>();
        public List<Homework> homeworks { get; set; } = new List<Homework>();
        public Teacher(string name) : base(name)
        {
        }

        public void addSubject(Subject subjectToAdd)
        {
            subjects.Add(subjectToAdd);
        }
        public void removeSubject(Subject subjectToRemove)
        {
            subjects.Remove(subjectToRemove);
        }

        public void AssignId()
        {
            PersonalId = "T" + name.Substring(0, 1).ToUpper() +
                         name.Substring(name.IndexOf(" ") + 1, 2).ToUpper();
        }

        public void AddHomeWork(Homework homework)
        {
            homeworks.Add(homework);
        }
        public List<Homework> GetHomeworks()
        {
            return homeworks;
        }

        public void GiveMark(Mark mark, Student student)
        {
            student.AddMark(mark);
        }
    }
}
