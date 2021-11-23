using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Marks;

namespace Core.Users
{
    public class Teacher : User
    {
        public List<Subject> subjects = new List<Subject>();
        private List<Homework> homeworks { get; }
        public Teacher(string name) : base(name)
        {
            homeworks = new List<Homework>();
            AssignId();
        }

        public Teacher()
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

        private void AssignId()
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
        public void AddExam(StudentClass classForExam, Subject subject, DateTime timeOfExam)
        {
            classForExam.Students.ForEach(student => student.AddExam(subject, timeOfExam));
        }

        public void GiveMark(Mark mark, Student student)
        {
            student.AddMark(mark);
        }
    }
}
