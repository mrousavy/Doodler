using DoodlerCore;
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

        //id of user
        int id;

        //User
        User user;


        public override List<string> CreateArgs()
        {
            throw new NotImplementedException();
        }

        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {

            using (var service = Statics.NewService())
            {
                user = await service.GetUserByIdAsync(id);
                

            }

            pwdata = user.Password;     

            try
            {
                if (pwcheck.Equals(pwdata))
                {
                    //update in database
                    user.Password = pwuser;


                }
                else
                {
                    throw new Exception();
                }

                return (await Task.FromResult(0));
            }
            catch (Exception e)
            {
                //User wrote wrong password
                

                return (await Task.FromResult(1));
            }
      }
   }
}

