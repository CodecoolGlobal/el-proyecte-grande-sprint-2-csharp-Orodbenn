using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    class Teacher : User
    {
        public Teacher(string name, int age, string phoneNumber, string personalId) : base(name, age, phoneNumber, personalId)
        {

        }
    }
}
