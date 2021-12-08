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
        public int mark { get; set; }
        public Teacher assigningTeacher { get; set; }
        public Subject subject { get; set; }
        public DateTime dateOfAssignment { get; set; }
        public MarkWeight weight { get; set; }

        public Mark()
        {
        }

        public Mark(int mark, Teacher assigningTeacher, Subject subject, MarkWeight weight)
        {
            this.mark = mark;
            this.assigningTeacher = assigningTeacher;
            this.subject = subject;
            this.weight = weight;
            dateOfAssignment = DateTime.Now;
        }
    }
}