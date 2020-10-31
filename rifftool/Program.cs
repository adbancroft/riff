using System.CommandLine;

namespace rifftool
{

    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand("RIFF file chunk editing")
            {
                new ParseCommand(),
            };

            return rootCommand.InvokeAsync(args).Result;
        }

    }
}
