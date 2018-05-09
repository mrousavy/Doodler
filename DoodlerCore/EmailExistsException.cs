using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodlerCore
{
    public class EmailExistsException : Exception
    {
        public string Email { get; set; }

        public EmailExistsException(string email) : base("The given email address already exists!")
        {
            Email = email;
        }
    }
}
