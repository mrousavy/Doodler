using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doodler.CLI.Commands
{
    [Command(Description = "Install Doodler to the Registry", Name = "install")]
    public class InstallCommand : CommandBase
    {
        public override List<string> CreateArgs()
        {
            throw new NotImplementedException();
        }

        protected override Task<int> OnExecuteAsync(CommandLineApplication app)
        {

            throw new NotImplementedException();
        }

       
    }
}
