using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    public class PollTests : IDisposable
    {
        public User CurrentUser { get; set; }

        public PollTests()
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
        [InlineData("Test: Is [person] nice?")]
        [InlineData("Test: Is the weather nice today?")]
        [InlineData("Test: Is Doodler well implemented?")]
        public async Task TestCreateTextPoll(string title)
        {
            using (var service = Statics.NewService())
            {
                var answers = new List<TextAnswer>
                {
                    new TextAnswer { Text="Yes" },
                    new TextAnswer { Text="No" }
                };
                await service.CreatePollAsync(CurrentUser, title, DateTime.Now.AddDays(10), answers);
            }
        }
    }
}
