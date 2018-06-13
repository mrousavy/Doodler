
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using McMaster.Extensions.CommandLineUtils;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Report a poll", Name = "report")]
    public class ReportPollCommand : CommandBase
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

                Poll pollToReport;
                do
                {
                    int id = Prompt.GetInt("Poll to report:");
                    pollToReport = polls.SingleOrDefault(p => p.Id == id);
                } while (pollToReport == null);

                string reasoning = Prompt.GetString("Report message: ");

                using (var service = Statics.NewService())
                {

                    await service.ReportPollAsync(pollToReport, reasoning);
                    await service.SaveAsync();
                }

                app.Out.WriteLine($"Successfully reported Poll \"{pollToReport.Title}\"!");
                return 0;
            }
            catch (Exception ex)
            {
                app.Out.WriteLine($"Could not find Poll! {ex.Message}");
                return 1;
            }
        }
    }
}

