using McMaster.Extensions.CommandLineUtils;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Register to the Doodler system", Name = "register")]
    public class RegisterCommand
    {
        [Argument(0, Description = "The email of the user to register to")]
        public string Email { get; }

        [Argument(1, Description = "The username of the user to register to")]
        public string Username { get; }

        public void Login(CommandLineApplication app)
        {
            string password = Prompt.GetPassword("Password:");
            string confirmPassword = Prompt.GetPassword("Confirm Password:");
            if (password == confirmPassword)
            {
                // TODO: Sign in
            }
        }
    }
}
