using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    public class TestDeletePoll : IDisposable
    {
        public User CurrentUser { get; set; }

        public TestDeletePoll()
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
        [InlineData("Test: Is Bob nice?", "yes", "kinda", "no, not really")]
        [InlineData("Test: Is the weather nice today?", "no", "it's london what do you expect", "yes")]
        [InlineData("Test: Is Doodler well implemented?", "yes", "of course", "perfect as always")]
        public async Task TestDeleteTextPoll(string title, params string[] answers)
        {
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                var textAnswers = answers.Select(a => new TextAnswer(a));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);

                IList<Answer> pollAnwers;
                pollAnwers = await service.GetAnswersForPollAsync(poll);

                foreach (var answer in pollAnwers)
                    await service.DeleteAnswerAsync(answer);

                await service.DeletePollAsync(poll);
                await service.SaveAsync();
            }
        }
    }
}
