using DoodlerCore;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Create a new Poll", Name = "create")]
    public class CreatePollCommand : CommandBase
    {
        public enum PollType
        {
            Date,
            Text
        }

        [Argument(0, Description = "The Poll's type <Date|Text>")]
        public PollType Type { get; set; }

        [Option(Description = "The amount of days until this poll gets closed")]
        public int DaysToClose { get; set; } = 7;


        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var user = await Statics.LoginUser();

            try
            {
                // Ask title
                string title = Prompt.GetString($"What's the title of the {Type} poll?:");
                if (string.IsNullOrWhiteSpace(title))
                    throw new Exception("Invalid Title!");

                var answers = new List<Answer>();
                bool addAnother;
                do
                {
                    // Add answers
                    string answer = Prompt.GetString("Answer: ");
                    if (Type == PollType.Date)
                    {
                        // Date answer
                        bool successful = DateTime.TryParse(answer, out var date);
                        if (successful)
                            answers.Add(new DateAnswer(date));
                        else
                            app.Out.WriteLine("Invalid date time! Format: dd.MM.yyyy");
                    } else
                    {
                        // Text answer
                        answers.Add(new TextAnswer(answer));
                    }

                    addAnother = Prompt.GetYesNo("Do you want to add another Answer?", true);
                } while (addAnother);

                // Create poll
                Poll poll;
                using (var service = Statics.NewService())
                {
                    poll = await service.CreatePollAsync(user, title, DateTime.Now.AddDays(DaysToClose), answers);
                }

                app.Out.WriteLine($"Successfully created Poll! Id: {poll.Id}");
                return 0;
            } catch (Exception ex)
            {
                app.Out.WriteLine($"Error creating poll! {ex.Message}");
                return 1;
            }
        }

        public override List<string> CreateArgs()
        {
            // TODO: CreateArgs()
            return new List<string>();
        }
    }
}
