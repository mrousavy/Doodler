using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Register to the Doodler system", Name = "register")]
    public class RegisterCommand : CommandBase
    {
        [Argument(0, Description = "The email of the user to register to")]
        public string Email { get; set; }

        [Argument(1, Description = "The username of the user to register to")]
        public string Username { get; set; }


        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }

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
                if (string.IsNullOrWhiteSpace(Username))
                {
                    throw new Exception("Invalid Username!");
                }
                string password = Prompt.GetPassword("Password:");
                string confirmPassword = Prompt.GetPassword("Confirm Password:");
                if (password == confirmPassword)
                {
                    // Register
                    using (var service = Statics.NewService())
                    {
                        Statics.CurrentUser = await service.RegisterAsync(Email, Username, password);
                    }

                    // Successfully registered
                    Statics.Preferences.LastEmail = Email;
                    Statics.Preferences.LastPassword = password;
                    app.Out.WriteLine($"Hello, {Statics.CurrentUser.Username}!");
                    return 0;
                } else
                {
                    throw new Exception("Passwords don't match!");
                }
            } catch (Exception e)
            {
                // Error on register
                app.Out.WriteLine($"Error registering! {e.Message}");
                return 1;
            }
        }
    }
}
