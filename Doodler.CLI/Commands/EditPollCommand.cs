﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using McMaster.Extensions.CommandLineUtils;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Edit a poll", Name = "edit")]
    public class EditPollCommand : CommandBase
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

                Poll pollToEdit;
                do
                {
                    int id = Prompt.GetInt("Poll to edit:");
                    pollToEdit = polls.SingleOrDefault(p => p.Id == id);
                } while (pollToEdit == null);

                pollToEdit.Title = Prompt.GetString("Enter new title:", pollToEdit.Title);

                string dateString = Prompt.GetString("Enter a new end-date (dd.MM.yyyy):", pollToEdit.EndsAt.ToString("dd.MM.yyyy"));
                pollToEdit.EndsAt = DateTime.Parse(dateString);
                
                using (var service = Statics.NewService())
                {
                    var currentAnswers = await service.GetAnswersForPollAsync(pollToEdit);

                    // foreach (var answer in currentAnswers)
                    //   await service.DeleteAnswerAsync(answer);
                }

                //TODO: isTextPoll
                bool isTextPoll = true;
                var answers = new List<Answer>();
                bool addAnother;
                do
                {
                    // Add answers
                    string answer = Prompt.GetString("Answer: ");
                    if (isTextPoll)
                    {
                        // Text answer
                        answers.Add(new TextAnswer(answer));
                    } else
                    {
                        // Date answer
                        bool successful = DateTime.TryParse(answer, out var date);
                        if (successful)
                            answers.Add(new DateAnswer(date));
                        else
                            app.Out.WriteLine("Invalid date time! Format: dd.MM.yyyy");
                    }

                    addAnother = Prompt.GetYesNo("Do you want to add another Answer?", true);
                } while (addAnother);
                
                app.Out.WriteLine($"Successfully edited Poll \"{pollToEdit.Title}\"!");
                return 0;
            }
            catch (Exception ex)
            {
                app.Out.WriteLine($"Could not edit Poll! {ex.Message}");
                return 1;
            }
        }
    }
}