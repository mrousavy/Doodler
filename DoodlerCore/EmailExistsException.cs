using System;

namespace DoodlerCore
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException(string email) : base("The given email address already exists!")
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}