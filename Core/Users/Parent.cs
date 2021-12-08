using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class Parent : User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public HashSet<Student> Students { get; set; } = new HashSet<Student>();

        public Parent(string name, Student student) : base(name)
        {
            student.parents.Add(this);
            Students.Add(student);
            AssignId(student);
        }
        
        public Parent()
        {
        }

        public void AssignId(Student student)
        {
            string id = student.PersonalId;
            id = id.Substring(0, 1).Replace("S", "P");
            PersonalId = id;
        }
    }
}
