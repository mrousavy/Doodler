using System;

namespace DoodlerCore
{
    public class InvalidCredentialsException : Exception
    {
        public string Email { get; set; }

        public InvalidCredentialsException(string email) : base("Invalid email or password!")
        {
            Email = email;
        }
    }
}
