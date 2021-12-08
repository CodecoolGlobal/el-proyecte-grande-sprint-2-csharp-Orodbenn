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
        private int mark { get; set; }

        private Teacher assigningTeacher { get; set; }
        private Subject subject { get; set; }
        private DateTime dateOfAssignment { get; set; }
        private MarkWeight weight { get; set; }

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