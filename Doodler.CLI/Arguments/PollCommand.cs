using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Create a new Poll", Name = "poll")]
    public class PollCommand : CommandBase
    {
        public enum PollType
        {
            Date,
            Text
        }

        [Option(Description = "The Poll type <Date|Text>")]
        public PollType Type { get; }


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
