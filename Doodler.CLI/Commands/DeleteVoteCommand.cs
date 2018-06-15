using DoodlerCore;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    /// <summary>
    /// Author: Fatih Aydin
    /// The DeleteVoteCommand class is called, when the user tries to remove a vote. 
    /// </summary>
    [Command(Description = "Delete a vote", Name = "unvote")]
    public class DeleteVoteCommand : CommandBase
    {
        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }

        protected async override Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                User user;
                Vote vote;
                Poll poll;

                using (var service = Statics.NewService())
                {
                    int id = Prompt.GetInt("Poll to delete:");
                    poll = await service.GetPollByIdAsync(id);
                    vote = await service.GetVoteFromUserForPoll(Statics.CurrentUser, poll);
                }

                using (var service = Statics.NewService())
                {
                    await service.DeleteVoteAsync(vote);
                }
                
                app.Out.WriteLine($"Successfully deleted Vote!");
                return 0;
            }
            catch (Exception ex)
            {
                app.Out.WriteLine($"Could not delete Vote! {ex.Message}");
                return 1;
            }
        }

    }
}
