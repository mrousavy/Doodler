using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    public class PollTests : IDisposable
    {
        public User CurrentUser { get; set; }
        public Random Random { get; set; }

        public PollTests()
        {
            CurrentUser = Statics.LoginUser().GetAwaiter().GetResult();
            Random = new Random();
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
        [InlineData("Test: Is [person] nice?")]
        [InlineData("Test: Is the weather nice today?")]
        [InlineData("Test: Is Doodler well implemented?")]
        public async Task TestCreateTextPoll(string title, params string[] answers)
        {
            Poll poll;
            // Create poll
            using (var service = Statics.NewService())
            {
                var textAnswers = answers.Select(a => new TextAnswer(a));
                poll = await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), textAnswers);
                await service.SaveAsync();
            }

            // Find poll
            using (var service = Statics.NewService())
            {
                var foundPoll = await service.GetPollByIdAsync(poll.Id);

                Assert.NotNull(foundPoll);
                Assert.Equal(poll.Id, foundPoll.Id);
            }
        }

        [Theory]
        [InlineData("mrousavy", "mrousavy@mail.com")]
        [InlineData("dmaniak", "dmaniak@mail.com")]
        [InlineData("mceylan", "mceylan@mail.com")]
        public async Task TestCreateUser(string name, string email)
        {
            User user;
            // Create user
            using (var service = Statics.NewService())
            {
                user = await service.RegisterAsync(email, name, Random.Next(1111111, 9999999).ToString());
                await service.SaveAsync();
            }

            // Find user
            using (var service = Statics.NewService())
            {
                var foundUser = await service.GetUserByIdAsync(user.Id);

                Assert.NotNull(foundUser);
                Assert.Equal(user.Id, foundUser.Id);
            }
        }
    }
}
