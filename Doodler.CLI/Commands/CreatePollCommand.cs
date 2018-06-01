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

        [Option(Description = "The Poll's title")]
        public string Title { get; set; }

        [Option(Description = "The Date this Poll closes")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

        [Option(Description = "The Poll type <Date|Text>")]
        public PollType Type { get; set; }

        [Option(Description = "All possible poll answers (Date Answers have to be in format dd.MM.yyyy)")]
        public string[] Answers { get; set; }


        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var user = await Statics.LoginUser();

            try
            {
                IEnumerable<Answer> answers;
                if (Type == PollType.Date)
                {
                    answers = Answers.Select(a => new DateAnswer(DateTime.Parse(a)));
                } else
                {
                    answers = Answers.Select(a => new TextAnswer(a));
                }

                Poll poll;
                using (var service = Statics.NewService())
                {
                    poll = await service.CreatePollAsync(user, Title, EndDate, answers);
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
