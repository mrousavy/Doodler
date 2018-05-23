using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var user = await Statics.LoginUser();
            // TODO: Vote on poll

            return 0;
        }
    }
}
