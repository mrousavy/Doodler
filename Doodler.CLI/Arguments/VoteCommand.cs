using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace Doodler.CLI.Arguments
{
    [Command(Description = "Vote on a given Poll", Name = "vote")]
    public class VoteCommand : CommandBase
    {
        [Argument(0, Description = "The Poll ID")]
        public int Id { get; }


        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            return 0;
        }
    }
}
