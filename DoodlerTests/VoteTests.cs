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
        //user for the poll
        public User CurrentUser { get; set; }

        public VoteTests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
        }

        //Fact what the test should return
        [Fact]
        public void testfact()
        {
            Assert.True(true);


        }

        /// <summary>
        /// Tests if votes can be seen from the system
        /// </summary>
        /// <param name="title"> Title of the poll thats beeing tested </param>
        /// <param name="answers"> String array with the answers </param>
        /// <returns> Returns if the test was successful </returns>
        
        public async Task TestVotes(string title, params string[] answers)
        {
            IList<DoodlerCore.Vote> vote;
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                //creating answers for the poll
                var textAnswers = answers.Select(answer => new TextAnswer(answer));

                //creating the poll from the information 
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);

                //saveing the poll
                await service.SaveAsync();
            }



            //Checking Votes
            using (var service = Statics.NewService())
            {
                //getting votes from the poll
                vote = await service.GetVotesForPollAsync(poll);
             
            }
            //Checking if the there are votes in the poll
            Assert.NotEmpty(vote);
        }
    }
}
