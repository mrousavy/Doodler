using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var user = await Statics.LoginUser();
            // TODO: Create poll

            return 0;
        }

        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }
    }
}
