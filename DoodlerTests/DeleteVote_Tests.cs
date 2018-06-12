using System;
using System.Collections.Generic;
using System.Text;
using DoodlerCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    public class DeleteVote_Tests
    {
        public User CurrentUser { get; set; }

        public DeleteVote_Tests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void TestFact()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("Test: Is [person] nice?", "yes", "kinda", "no, not really")]
        [InlineData("Test: Is the weather nice today?", "no", "it's london what do you expect", "yes")]
        [InlineData("Test: Is Doodler well implemented?", "yes", "of course", "perfect as always")]
        public async Task TestDeleteVote(string title, params string[] answers)
        {
            Poll poll;
            Vote vote;

            // Create poll
            using (var service = Statics.NewService())
            {
                var textAnswers = answers.Select(a => new TextAnswer(a));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);
                await service.SaveAsync();

                IList<Answer> PollAnswers = await service.GetAnswersForPollAsync(poll);
                Answer PollAnswer = PollAnswers.First();
                
                await service.VoteOnPoll(CurrentUser, poll, PollAnswer);
                
                //Get Vote
                vote = await service.GetVoteFromUserForPoll(CurrentUser, poll);

                Assert.NotNull(vote);

                //Delete Vote
                await service.DeleteVoteAsync(vote);

                Vote CurrentVote = await service.GetVoteFromUserForPoll(CurrentUser, poll);
                Assert.Null(CurrentVote);
            }
        }
    }
}
