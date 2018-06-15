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
            return null;
        }

        protected override Task<int> OnExecuteAsync(CommandLineApplication app)
        {

            return null;
        }

       
    }
}
