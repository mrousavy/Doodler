using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Login to a given account", Name = "login")]
    public class LoginCommand : CommandBase
    {
        [Argument(0, Description = "The email of the user to sign in to")]
        public string Email { get; set; }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch
            {
                return false;
            }
        }

        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                // Check credentials & sign in
                if (!IsValidEmail(Email))
                {
                    throw new Exception("Invalid Email Address!");
                }
                string password = Prompt.GetPassword("Password:");
                using (var service = Statics.NewService())
                {
                    Statics.CurrentUser = await service.LoginAsync(Email, password);
                }

                // Successfully signed in
                Statics.Preferences.LastEmail = Email;
                Statics.Preferences.LastPassword = password;
                app.Out.WriteLine($"Hello, {Statics.CurrentUser.Username}!");
                return 0;
            } catch (Exception e)
            {
                // Error on sign in
                app.Out.WriteLine($"Error signing in! {e.Message}");
                return 1;
            }
        }

        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }
    }
}
