using System;
using System.Collections.Generic;
using System.Text;
using DoodlerCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DoodlerCore.Exceptions;

namespace DoodlerTests
{
    /// 
    /// Author: Fatih Aydin
    /// UnitTests for the delete poll functionality 
    ///
    public class DeletePoll_Tests : IDisposable
    {
        //the current user for the poll
        public User CurrentUser { get; set; }

        public DeletePoll_Tests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
        }

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
        /// Tests the functionality for polls and whether deleting them works.
        /// </summary>
        /// <param name="title"> Title of the poll that is being tested </param>
        /// <param name="answers"> A String Arrays that contains the answers </param>
        /// <returns> Returns if the test was successfull </returns>
        [Theory]
        [InlineData("Test: Is [person] nice?", "yes", "kinda", "no, not really")]
        [InlineData("Test: Is the weather nice today?", "no", "it's london what do you expect", "yes")]
        [InlineData("Test: Is Doodler well implemented?", "yes", "of course", "perfect as always")]
        public async Task TestDeletePoll(string title, params string[] answers)
        {
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                var textAnswers = answers.Select(a => new TextAnswer(a));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);
                // save poll
                await service.SaveAsync();
            }
            int id = poll.Id;

            using (var service = Statics.NewService())
            {
                poll = await service.GetPollByIdAsync(id);

                //Delete Poll
                await service.DeletePollAsync(poll);
                // save after deletion
                await service.SaveAsync();
            }

            using (var service = Statics.NewService())
            {
                await Assert.ThrowsAsync<PollNotFoundException>(async () => await service.GetPollByIdAsync(id));
            }
        }
    }
}
