using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    /// <summary>
    ///     mrousavy: XUnit tests for a given poll
    /// </summary>
    public class DeleteVoteTests : IDisposable
    {
        public User CurrentUser { get; set; }

        public DeleteVoteTests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void TestFact()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        public async Task TestVoteRandomOnPoll(int pollId, int answerId)
        {
            Poll poll;
            IEnumerable<Vote> votesBefore;
            // Create poll
            using (var service = Statics.NewService())
            {
                poll = await service.GetPollByIdAsync(pollId);
                votesBefore = await service.GetVotesForPollAsync(poll);
                var answers = await service.GetAnswersForPollAsync(poll);
                await service.VoteOnPoll(CurrentUser, poll, answers[answerId]);
            }

            // Compare votes
            using (var service = Statics.NewService())
            {
                IEnumerable<Vote> votesAfter = await service.GetVotesForPollAsync(poll);

                // after should be more
                Assert.NotEqual(votesAfter.Count(), votesBefore.Count());
            }
        }
    }
}
