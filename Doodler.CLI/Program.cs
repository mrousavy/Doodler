using Doodler.CLI.Commands;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Doodler.CLI
{
    [Command("doodler")]
    [VersionOption("--version", "1.0.0")]
    [Subcommand("login", typeof(LoginCommand))]
    [Subcommand("register", typeof(RegisterCommand))]
    [Subcommand("create", typeof(CreatePollCommand))]
    [Subcommand("vote", typeof(VoteCommand))]
<<<<<<< HEAD
    [Subcommand("edit", typeof(EditPollCommand))]
=======
    [Subcommand("delete", typeof(DeletePollCommand))]
>>>>>>> 7f3ad37857289967f2f5e6e30ce4082cdf230a99
    public class Program : CommandBase
    {
        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var user = await Statics.LoginUser();
            app.Out.WriteLine($"Doodler - Signed in as {user.Username} (on {Statics.Server}.{Statics.Database})");
            // this shows help even if the --help option isn't specified
            app.ShowHelp();
            return 1;
        }

        public override List<string> CreateArgs()
        {
            var args = new List<string>();
            if (Statics.CurrentUser != null)
            {
                args.Add("--user=" + Statics.CurrentUser.Username);
            }
            return args;
        }

        // MAIN ENTRY POINT
        public static int Main(string[] args)
        {
            int exitCode = CommandLineApplication.Execute<Program>(args);

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            return exitCode;
        }
    }
}
