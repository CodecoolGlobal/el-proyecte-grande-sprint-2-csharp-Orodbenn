using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public abstract class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "User";
        public string PersonalId { get; set; }

        protected User(string name)
        {
            this.name = name;
        }

        protected User(string name, DateTime dateOfBirth)
        {
            this.name = name;
            DateOfBirth = dateOfBirth;
            Password = "password123";
            Role = "User";
        }

        protected User()
        {
            Role = "User";
        }
    }
}
