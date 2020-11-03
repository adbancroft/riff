using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;

namespace rifftool
{

    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand("RIFF file chunk browsing and editing")
            {
                new ParseCommand(),
                new AddChunkCommand(),
                new DumpChunkCommand()
            };
            return rootCommand.Invoke(args);
        }

        private static void ExceptionHandler(Exception exception, InvocationContext context)
        {
            Console.Error.WriteLine(exception.Message);
        }
    }
}
