using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff.Write.Chunk;

namespace rifftool
{
    class AddChunkCommand : Command
    {
        public AddChunkCommand()
            : base("addchunk", "Add a chunk to a RIFF file")
        {
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
            this.AddMutuallyExclusiveRequired(
                new Option<string>("--data", "The data for the new chunk.") 
                { 
                },
                new Option<FileInfo>("--filedata", "Path to a file containing The data for the new chunk.") 
                { 
                }.ExistingOnly());

            Handler = CommandHandler.Create<FileInfo, FileInfo, string, string, string, FileInfo>(Add);
        }

        private static int Add(FileInfo input, FileInfo output, string parent, string name, string data, FileInfo filedata)
        {
            try {
                var writeChunks = input.ReadRiff().CreateWriteChunk();
                var newChunk = new ByteArrayChunk { Identifier = name, Data = CreateChunkData(data, filedata) };
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

        private static byte[] CreateChunkData(string data, FileInfo filedata)
        {
            if (filedata!=null)
            {
                using (var sourceStream = new FileStream(filedata.FullName, FileMode.Open))
                using (var ms = new MemoryStream())
                {
                    sourceStream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            
            return System.Text.Encoding.ASCII.GetBytes(data);
        }
    }
}