using Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [Serializable]
    public class Homework
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public StudentClass studentClass { get; set; }
        public Subject Subject { get; set; }
        public string description { get; set; }
        DateTime date { get; set; }

        public Homework(StudentClass studentClass, Subject subject, String desc)
        {
            this.studentClass = studentClass;
            Subject = subject;
            date = DateTime.Now;
            description = desc;
        }
        public Homework()
        {
        }
    }
}
