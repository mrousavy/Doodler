using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.CLI
{
    [HelpOption("--help")]
    public abstract class CommandBase
    {
        public abstract List<string> CreateArgs();
        protected abstract Task<int> OnExecuteAsync(CommandLineApplication app);
    }
}
