using System;
using System.Collections.Generic;
using System.Text;
using DoodlerCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace DoodlerTests
{
    public class DeletePoll_Tests : IDisposable
    {
        public User CurrentUser { get; set; }

        public DeletePoll_Tests()
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
        public async Task TestDeletePoll(string title, params string[] answers)
        {
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                var textAnswers = answers.Select(a => new TextAnswer(a));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);
                await service.SaveAsync();

                //Delete Poll
                await service.DeletePollAsync(poll);
                await service.SaveAsync();

                Assert.Null(poll);
            }
        }
    }
}
