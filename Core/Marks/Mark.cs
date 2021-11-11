using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Users;

namespace Core.Marks
{
   public class Mark
   {
       private int mark;
       private Teacher assigningTeacher;
       private Subject subject;
       private DateTime dateOfAssignment;
       private MarkWeight weight;

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
