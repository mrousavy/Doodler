using DoodlerCore;
using DoodlerCore.Exceptions;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
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
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Doodler.CLI.Commands
{
    [Command(Description = "Vote on a given Poll", Name = "vote")]
    public class VoteCommand : CommandBase
    {
        [Argument(0, Description = "The Poll ID")]
        public int Id { get; }
            Poll poll;
            IEnumerable<Answer> answers;
            try
            {

                using (var service = Statics.NewService())
                {
                    poll = await service.GetPollByIdAsync(Id);
                    // TODO: Get answers
                }
            }
            catch (PollNotFoundException)
            {
                app.Out.WriteLine("This Id is not valid");
                return 1;
            }
            // TODO: Ask user which answer (via index?)
            int index = Prompt.GetInt("answer??");
            // TODO: Vote on Poll

                return 0;
        }

    }
}
