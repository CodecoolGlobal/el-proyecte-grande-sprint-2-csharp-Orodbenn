using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    class Parent : User
    {
        public HashSet<Student> students { get; set; }

        public Parent(string name, int age, string phoneNumber, string personalId) : base(name, age, phoneNumber, personalId)
        {
        }
    }
}
