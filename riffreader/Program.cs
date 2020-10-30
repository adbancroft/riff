using Riff.Read.Chunk;
using System.IO;
using System.Linq;
using Riff.Write.Chunk;

namespace riffreader
{
    class Program
    {
        private static ChunkDescriptorBase ReadFile(string path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                return Riff.Read.Reader.Read(reader, new Riff.Read.LazyBasicChunkFactory(reader, new FileStreamProvider(path)));
            }
        }

        static void Main(string[] args)
        {
            string readPath = @"//Desktop-27m6eeq/d/Users/Bancrofts/pictures/2006/Andrews Mum & Dad\Mum in pool - 1.AVI";
            //string path = @"C:\scratch\test.riff";
            var riffChunk = ReadFile(readPath);

            var hdr = riffChunk.CreateWriteChunk();
            var header = hdr.WhereListType("hdrl").First();
            header.Add(new StringChunk { Identifier = "IDIT", Data="THIS IS A TEST"});
            //header.Add(new RawChunk { Identifier="IDIT", Data = })
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
