using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Logouts from the account", Name = "logout")]
    public class LogoutCommand : CommandBase
    {
        public override List<string> CreateArgs()
        {
            throw new NotImplementedException();
        }

        protected override Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                

                Statics.Preferences.LastEmail = null;
                Statics.Preferences.LastPassword = null;
                Statics.CurrentUser = null;
                app.Out.WriteLine("You sucessfully logged out");
                app.Out.WriteLine("Have a nice day!");
            }
            catch (Exception e)
            {
                //Error on Logout
                app.Out.WriteLine($"Error Logging in! {e.Message}");
               
            }
        }
    }
}
