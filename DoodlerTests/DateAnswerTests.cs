using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoodlerCore;
using Xunit;

namespace DoodlerTests
{
    public class DateAnswerTests : IDisposable
    {
        

        public DateAnswerTests()
        {
            

        }

        public void Dispose()
        {
        }

        //checks if the test is successful
        [Fact]
        public void testfact()
        {
            Assert.True(true);
        }

        /// <summary>
        /// Test if the dateanswer constructor works
        /// </summary>
        /// <param name="datetime">the datetime used for dateanswer</param>
        /// <returns></returns>

        public async Task TestDateAnswers(DateTime datetime)
        {
            //the Dateanswer thats beeing checked
            DateAnswer date;
             
            using(var servie = Statics.NewService())
            {
                //creating a new Dateanswer with the datetime
                date = new DateAnswer(datetime);
                

            }
            // checking if DateAnswer() 
            Assert.NotNull(date);
        }

    }
}
