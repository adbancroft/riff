using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.IO;

namespace rifftool
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var rootCommand = new RootCommand("RIFF file chunk browsing and editing")
            {
                new ParseCommand(),
                new AddChunkCommand(),
                new DumpChunkCommand(),
                new DeleteChunkCommand()
            };
            rootCommand.AddGlobalOption(new Option<FileInfo>(new [] {"--input", "-i"}, "The RIFF file to read")
                {
                    IsRequired = true
                }.ExistingOnly());
            return rootCommand.Invoke(args);
        }
    }
}
