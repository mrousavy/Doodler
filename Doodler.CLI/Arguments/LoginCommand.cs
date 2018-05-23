using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Login to a given account", Name = "login")]
    public class LoginCommand : CommandBase
    {
        [Argument(0, Description = "The email of the user to sign in to")]
        public string Email { get; }

        protected override int OnExecute(CommandLineApplication app)
        {
            string password = Prompt.GetPassword("Password:");
            // TODO: Sign in

            return 0;
        }

        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }
    }
}
