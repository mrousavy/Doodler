using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using McMaster.Extensions.CommandLineUtils;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Delete a poll", Name = "delete")]
    public class DeletePollCommand : CommandBase
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
                IList<Poll> polls;
                using (var service = Statics.NewService())
                {
                    polls = await service.GetAllPollsAsync();
                }

                foreach (var poll in polls)
                {
                    app.Out.WriteLine($"{poll.Id}: {poll.Title}");
                }

                // TODO: Check if ID exists

                Poll pollToDelete;
                do
                {
                    int id = Prompt.GetInt("Poll to delete:");
                    pollToDelete = polls.SingleOrDefault(p => p.Id == id);
                } while (pollToDelete == null);


                IList<Answer> answers;
                using (var service = Statics.NewService())
                {
                    answers = await service.GetAnswersForPollAsync(pollToDelete);
                }

                int count = answers.Count;
                if (count > 0)
                {
                    bool delete = Prompt.GetYesNo($"The Poll has {count} Vote(s) already submitted, delete anyway?", false);
                    if (!delete)
                        return 0;
                }

                using (var service = Statics.NewService())
                {
                    foreach (var answer in answers)
                        await service.DeleteAnswerAsync(answer);
                    await service.DeletePollAsync(pollToDelete);
                    await service.SaveAsync();
                }

                app.Out.WriteLine($"Successfully deleted Poll \"{pollToDelete.Title}\"!");
                return 0;
            }
            catch (Exception ex)
            {
                app.Out.WriteLine($"Could not delete Poll! {ex.Message}");
                return 1;
            }
        }
    }
}
