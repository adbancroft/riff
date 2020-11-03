using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff.Read;
using Riff.Read.Chunk;
using Riff.Write;
using Riff.Write.Chunk;
using System.Linq;
using Validation;
using System.Collections.Generic;
using System.CommandLine.Parsing;

namespace rifftool
{
    class AddChunkCommand : Command
    {
        public AddChunkCommand()
            : base("addchunk", "Add a chunk to a RIFF file")
        {
            AddOption(new Option<FileInfo>(new [] {"--input", "-i"}, "The RIFF file to read") 
                { 
                    IsRequired = true 
                }.ExistingOnly());
            AddOption(new Option<FileInfo>(new [] {"--output", "-o"}, "The path to wrie the updated RIFF file") 
                { 
                    IsRequired = true 
                });
            AddOption(new Option<string>("--parent", () => String.Empty, "Slash delimited path of new chunks parent. E.g. LIST-hdrl-1\\INFO") 
                { 
                    
                });
            AddOption(new Option<string>("--name", "The name of the new chunk. E.g. IDIT. Use a slash delimited path to add a hierarchy. E.g. INFO\\IDIT") 
                { 
                    IsRequired = true 
                });
            AddOption(new Option<string>("--data", "The data of the new chunk. Can be a string or a file path. In the case of a file path, the file contents are written to the chunk.") 
                { 
                    IsRequired = true 
                });                

            Handler = CommandHandler.Create<FileInfo, FileInfo, string, string, string>(Add);
        }

        private static int Add(FileInfo input, FileInfo output, string parent, string name, string data)
        {
            try {
                var writeChunks = input.ReadRiff().CreateWriteChunk();
                var newChunk = new StringChunk { Identifier = name, Data = data };
                writeChunks.FindChunk(parent).Add(newChunk);

                using (var writer = new BinaryWriter(new FileStream(output.FullName, FileMode.OpenOrCreate)))
                {
                    writeChunks.Write(writer);
                }

                return 0;
            }
            catch(Exception e)
            {
                Console.Error.Write(e.Message);
                return 1;
            }
        }
    }
}