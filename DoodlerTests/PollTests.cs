﻿using DoodlerCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoodlerTests
{
    /// <summary>
    ///     mrousavy: XUnit tests for a given poll
    /// </summary>
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
        [InlineData("Test: Is [person] nice?", "yes", "kinda", "no, not really")]
        [InlineData("Test: Is the weather nice today?", "no", "it's london what do you expect", "yes")]
        [InlineData("Test: Is Doodler well implemented?", "yes", "of course", "perfect as always")]
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
    }
}
