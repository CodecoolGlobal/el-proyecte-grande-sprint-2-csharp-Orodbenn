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


        private DateTime DateOfBirth { get; }
        public string PhoneNumber { get; set; }
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

        protected User()
        {

        }
    }
}
