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
        private DateTime DateOfBirth { get; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PersonalId { get; set; }

        protected User(string name)
        {
            this.name = name;
        }

        protected User(string name, DateTime dateOfBirth)
        {
            this.name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}