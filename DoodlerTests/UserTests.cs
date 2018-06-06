using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using Xunit;

namespace DoodlerTests
{
    public class UserTests : IDisposable
    {
        public Random Random { get; set; }

        public UserTests()
        {
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
