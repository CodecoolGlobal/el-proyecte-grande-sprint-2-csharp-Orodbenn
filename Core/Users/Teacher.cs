﻿using System;
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
        private List<Homework> homeworks = new List<Homework>();
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

        public void AddHomeWork(Homework homework)
        {
            homeworks.Add(homework);
        }

        public void AddExam(StudentClass classForExam, Subject subject, DateTime timeOfExam)
        {
            classForExam.Students.ForEach(student => student.AddExam(subject, timeOfExam));
        }

        public void GiveMark(Student student, byte mark, MarkWeight weight, Subject subject)
        {
            student.AddMark(new Mark(mark, this, subject, weight));
        }
    }
}
