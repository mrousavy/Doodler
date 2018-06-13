using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using Xunit;


namespace DoodlerTests
{
    public class VoteTests : IDisposable
    {

        public User CurrentUser { get; set; }

        public VoteTests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void testfact()
        {
            Assert.True(true);


        }

        public async Task TestVotes(string title, params string[] answers)
        {
            IList<DoodlerCore.Vote> vote;
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                
                var textAnswers = answers.Select(answer => new TextAnswer(answer));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);
                await service.SaveAsync();
            }



            //Checking Votes
            using (var service = Statics.NewService())
            {
                vote = await service.GetVotesForPollAsync(poll);
             
            }
            Assert.NotEmpty(vote);
        }
    }
}
