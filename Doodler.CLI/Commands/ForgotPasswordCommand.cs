using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "checks if the user knows his old password", Name = "forgotpassword")]
    class ForgotPasswordCommand : CommandBase
    {
        //new Password
        String pwuser;

        //User input of Old password
        String pwcheck;

        //password from database
        String pwdata;

        public override List<string> CreateArgs()
        {
            throw new NotImplementedException();
        }

        protected override Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                if (pwcheck.Equals(pwdata))
                {
                    //update in database
                    pwdata = pwuser;

                }
                else
                {
                    throw new Exception();
                }

                return (Task.FromResult(0));
            }
            catch (Exception e)
            {
                //User wrote wrong password
                


                return (Task.FromResult(1));
            }
      }
   }
}

