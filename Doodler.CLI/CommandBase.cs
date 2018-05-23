using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace Doodler.CLI
{
    [HelpOption("--help")]
    public abstract class CommandBase
    {
        public abstract List<string> CreateArgs();

        protected virtual int OnExecute(CommandLineApplication app)
        {
            var args = CreateArgs();

            Console.WriteLine("=> doodler " + ArgumentEscaper.EscapeAndConcatenate(args));
            return 0;
        }
    }
}
