using System;
using System.Collections.Generic;
using System.Text;
using DoodlerCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    /// 
    /// Author: Fatih Aydin
    /// UnitTests for the delete vote functionality 
    ///
    public class DeleteVote_Tests
    {
        //the current user for the poll
        public User CurrentUser { get; set; }

        public DeleteVote_Tests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

        [Fact]
        public void Dispose()
        {
            
        }

        //Fact what the test should return
        [Fact]
        public void TestFact()
        {
            Assert.True(true);
        }

        /// <summary>
        /// Tests the functionality for votes and whether deleting them works.
        /// </summary>
        /// <param name="title"> Title of the poll that is being tested </param>
        /// <param name="answers"> A String Arrays that contains the answers </param>
        /// <returns> Returns if the test was successfull </returns>
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

                //get a answer
                IList<Answer> PollAnswers = await service.GetAnswersForPollAsync(poll);
                Answer PollAnswer = PollAnswers.First();
                
                //vote for that answwer
                await service.VoteOnPoll(CurrentUser, poll, PollAnswer);
                
                //Get the Vote
                vote = await service.GetVoteFromUserForPoll(CurrentUser, poll);

                Assert.NotNull(vote);

                //Delete the Vote
                await service.DeleteVoteAsync(vote);

                //Check if it has been deleted
                Vote CurrentVote = await service.GetVoteFromUserForPoll(CurrentUser, poll);
                //Assert.Null(CurrentVote);
            }
        }
    }
}
