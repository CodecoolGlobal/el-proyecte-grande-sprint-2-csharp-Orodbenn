using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
   public abstract class User
    {
        public string name { get; set; }
        public int age { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalId { get; set; }

        protected User(string name, int age, string phoneNumber, string personalId)
        {
            this.name = name;
            this.age = age;
            PhoneNumber = phoneNumber;
            PersonalId = personalId;
        }
    }
}
