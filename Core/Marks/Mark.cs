using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Users;

namespace Core.Marks
{
    public class Mark
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public int MarkValue { get; set; }
        public Teacher AssigningTeacher { get; set; }
        public Subject Subject { get; set; }
        public DateTime DateOfAssignment { get; set; }
        public MarkWeight Weight { get; set; }

        public Mark(int markValue, Teacher assigningTeacher, Subject subject, MarkWeight weight)
        {
            this.MarkValue = markValue;
            this.AssigningTeacher = assigningTeacher;
            this.Subject = subject;
            this.Weight = weight;
            DateOfAssignment = DateTime.Now;
        }

        public Mark()
        {
        }
    }
}