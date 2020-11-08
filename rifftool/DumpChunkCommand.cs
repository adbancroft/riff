using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Riff;

namespace rifftool
{
    public class DumpChunkCommand : Command
    {
        public DumpChunkCommand()
            : base("dumpchunk", "Write chunk data to a file")
        {
            AddOption(new Option<FileInfo>(new [] {"--output", "-o"}, "The path to wrie the updated RIFF file. Default to stdout"));
            AddOption(new Option<string>("--chunkpath", () => String.Empty, "Slash delimited path of chunk to dump. E.g. LIST-hdrl-1\\INFO\\IDIT"));

            Handler = CommandHandler.Create<FileInfo, FileInfo, string>(Dump);
        }

        private static void Dump(FileInfo input, FileInfo output, string chunkpath)
        {
            var readChunks = input.ReadRiff();
            var writeChunks = readChunks.CreateWriteChunk();
            var dumpChunk = writeChunks.FindChunk(chunkpath);

            var outStream = Console.OpenStandardOutput();
            if (output!=null)
            {
                outStream = new FileStream(output.FullName, FileMode.OpenOrCreate);
            }
            using var writer = new BinaryWriter(outStream);
            dumpChunk.WriteData(writer);
        }
    }
}