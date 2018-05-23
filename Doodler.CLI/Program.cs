using Doodler.CLI.Arguments;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace Doodler.CLI
{
    [Command("doodler")]
    [VersionOption("--version", "1.0.0")]
    [Subcommand("login", typeof(LoginCommand))]
    [Subcommand("register", typeof(RegisterCommand))]
    [Subcommand("new", typeof(PollCommand))]
    [Subcommand("vote", typeof(VoteCommand))]
    public class Program : CommandBase
    {
        protected override int OnExecute(CommandLineApplication app)
        {
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

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return exitCode;
        }
    }
}
