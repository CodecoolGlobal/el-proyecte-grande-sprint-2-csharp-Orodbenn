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
       private byte mark;
       private Teacher assigningTeacher;
       private Subject subject;
       private DateTime dateOfAssignment;
       private MarkWeight weight;

       public Mark(byte mark, Teacher assigningTeacher, Subject subject, MarkWeight weight)
       {
           MarkValidation(mark);
           this.assigningTeacher = assigningTeacher;
           this.subject = subject;
           this.weight = weight;
           dateOfAssignment = DateTime.Now;
       }

       private void MarkValidation(byte mark)
       {
           if (mark >= 1 || mark <= 5)
           {
               this.mark = mark;
           }
           else
           {
               throw new Exception(); // TODO implement exception
           }
       }
   }
}
