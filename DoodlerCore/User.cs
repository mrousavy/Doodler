
namespace DoodlerCore
{
    /// <summary>
    ///     mrousavy: A User with email, username and password
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        // TODO: Bad raw password storing
        public string Password { get; set; }


        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
        public User()
        {
        }
    }
}
