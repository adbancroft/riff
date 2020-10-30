using Riff.Read.Chunk;
using System.IO;
using Riff.Write.Chunk.Lazy;

namespace riffreader
{
    class Program
    {
        private static ChunkDescriptorBase ReadFile(string path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                return Riff.Read.Reader.Read(reader, new Riff.Read.BasicChunkFactory(reader, new FileSourceStreamProvider(path)));
            }
        }

        static void Main(string[] args)
        {
            string readPath = @"//Desktop-27m6eeq/d/Users/Bancrofts/pictures/2006/Andrews Mum & Dad\Mum in pool - 1.AVI";
            //string path = @"C:\scratch\test.riff";
            var riffChunk = ReadFile(readPath);

            var hdr = riffChunk.CreateWriteChunk();
            // hdr.Identifier = riffChunk.Identifier;
            // hdr.ListType = riffChunk.ListType;
            // hdr.Add(new Riff.Write.Chunk.RawChunk { Identifier = "IDIT", Data = new byte[43] });
            // hdr.Add(new Riff.Write.Chunk.RawChunk { Identifier = "IDIT", Data = new byte[26] });
            string writePath = @"C:\scratch\test.avi";
            using (var writer = new BinaryWriter(new FileStream(writePath, FileMode.OpenOrCreate)))
            {
                hdr.Write(writer);
            }
        }
    }
}
