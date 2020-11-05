using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff.Read.Chunk;
using Newtonsoft.Json;

namespace rifftool
{
    class ParseCommand : Command
    {
        public ParseCommand()
            : base("parse", "Read the RIFF file structure")
        {
            Handler = CommandHandler.Create<FileInfo>(Parse);
        }

        
        private static int Parse(FileInfo input)
        {
            Console.Write(ToJson(input.ReadRiff()));
            return 0;
        }

        private static string ToJson(ChunkDescriptorBase chunk)
        {
            return JsonConvert.SerializeObject(chunk);
        }
    }
}
