using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Register to the Doodler system", Name = "register")]
    public class RegisterCommand : CommandBase
    {
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
                string email = Prompt.GetString("Enter your email address:");
                if (!IsValidEmail(email))
                {
                    throw new Exception("Invalid Email Address!");
                }

                string username = Prompt.GetString("Choose a display name or username:");
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception("Invalid Username!");
                }

                string password = Prompt.GetPassword("Choose a Password:");
                string confirmPassword = Prompt.GetPassword("Confirm Password:");
                if (password == confirmPassword)
                {
                    // Register
                    using (var service = Statics.NewService())
                    {
                        Statics.CurrentUser = await service.RegisterAsync(email, username, password);
                        await service.SaveAsync();
                    }

                    // Successfully registered
                    Statics.Preferences.LastEmail = email;
                    Statics.Preferences.LastPassword = password;
                    app.Out.WriteLine("Successfully registered!");
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
