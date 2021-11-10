﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Teacher : User
    {
        public List<Subject> subjects = new List<Subject>();
        private Dictionary<SchoolClass, Dictionary<DateTime, string>> homeworks { get; }

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

        public void AddHomeWork(SchoolClass classForAssignment, string description, Subject subject)
        {
            Dictionary<DateTime, string> homework = new Dictionary<DateTime, string>();
            homework.Add(DateTime.Now, description);
            
            homeworks.Add(classForAssignment, homework);
            classForAssignment.Students.ForEach(student => student.AddHomeWork(subject, homework));
        }

        public void AddExam(SchoolClass classForExam, Subject subject, DateTime timeOfExam)
        {
            classForExam.Students.ForEach(student => student.AddExam(subject, timeOfExam));
        }
    }
}
