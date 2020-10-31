using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff.Read;
using Riff.Read.Chunk;
using Newtonsoft.Json;

namespace rifftool
{
    class ParseCommand : Command
    {
        public ParseCommand()
            : base("parse", "Read the RIFF file structure")
        {
            var inputOption = 
                new Option<FileInfo>(new [] {"--input", "-i"}, "The RIFF file to read") 
                { 
                    IsRequired = true 
                }.ExistingOnly();
            AddOption(inputOption);

            Handler = CommandHandler.Create<FileInfo>(Parse);
        }

        
        private static int Parse(FileInfo input)
        {
            using (var reader = new BinaryReader(new FileStream(input.FullName, FileMode.Open)))
            {
                Console.Write(ToJson(Reader.Read(reader, new Riff.Read.BasicChunkFactory(reader))));
            }
            return 0;
        }

        private static string ToJson(ChunkDescriptorBase chunk)
        {
            return JsonConvert.SerializeObject(chunk);
        }
    }
}
