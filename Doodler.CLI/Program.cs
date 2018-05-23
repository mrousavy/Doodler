using McMaster.Extensions.CommandLineUtils;
using System;

namespace Doodler.CLI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication<Program> { Name = "Doodler" };
            // TODO: Select Command from Doodler.CLI.Arguments
            int exitCode = app.Execute(args);
            Console.ReadKey();
            return exitCode;
        }
    }
}
