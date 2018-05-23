using McMaster.Extensions.CommandLineUtils;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Login to a given account", Name = "login")]
    public class LoginCommand
    {
        [Argument(0, Description = "The email of the user to sign in to")]
        public string Email { get; }

        public void Login(CommandLineApplication app)
        {
            string password = Prompt.GetPassword("Password:");
            // TODO: Sign in
        }
    }
}
