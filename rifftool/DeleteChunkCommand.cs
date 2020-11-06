using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff.Write.Chunk;

namespace rifftool
{
    internal class DeleteChunkCommand : Command
    {
        public DeleteChunkCommand()
            : base("deletechunk", "Delete a chunk")
        {
            AddOption(new Option<FileInfo>(new [] {"--output", "-o"}, "The path to write the updated RIFF file")
                {
                    IsRequired = true
                });
            AddOption(new Option<string>("--deletepath", "Slash delimited path of chunk to delete. E.g. LIST-hdrl-1\\INFO")
                {
                    IsRequired = true
                });

            Handler = CommandHandler.Create<FileInfo, FileInfo, string>(Delete);
        }

        private static int Delete(FileInfo input, FileInfo output, string deletePath)
        {
                var rootChunk = input.ReadRiff().CreateWriteChunk();
                rootChunk.FindParent(deletePath).Remove(rootChunk.FindChunk(deletePath));

                using (var writer = new BinaryWriter(new FileStream(output.FullName, FileMode.OpenOrCreate)))
                {
                    rootChunk.Write(writer);
                }

                return 0;
        }
    }
}
