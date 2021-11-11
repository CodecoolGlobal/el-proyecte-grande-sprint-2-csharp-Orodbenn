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
        string description;
        DateTime date;

        public Homework(StudentClass studentClass, Subject subject, String desc)
        {
            this.studentClass = studentClass;
            Subject = subject;
            date = DateTime.Now;
            description = desc;
        }
    }
}
