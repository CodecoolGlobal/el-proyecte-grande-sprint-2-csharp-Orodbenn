using Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Homework
    {
        StudentClass studentClass;
        Subject Subject;
        Teacher Teacher;
        DateTime date;

        public Homework(StudentClass studentClass, Subject subject, Teacher teacher, DateTime date)
        {
            this.studentClass = studentClass;
            Subject = subject;
            Teacher = teacher;
            this.date = date;
        }
    }
}
