using System.IO.MemoryMappedFiles;
using Riff;
using System.IO;
using Newtonsoft.Json;

namespace riffreader
{
    class Program
    {        
        static void Main(string[] args)
        {
            string path = @"//Desktop-27m6eeq/d/Users/Bancrofts/pictures/2006/Andrews Mum & Dad\Mum in pool - 1.AVI";
            //string path = @"C:\scratch\test.riff";
            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile (path))
            {
                using (var viewStream = mmf.CreateViewStream())
                {
                    using (var reader = new BinaryReader(viewStream, System.Text.Encoding.ASCII, false))
                    {
                        var riffChunk = Riff.Read.Reader.Read(reader, new Riff.Read.BasicChunkFactory());
                        System.Console.Write(JsonConvert.SerializeObject(riffChunk));

                        var hdr = new Riff.Write.Chunk.ListChunk();
                        hdr.Identifier = riffChunk.Identifier;
                        hdr.ListType = riffChunk.ListType;
                        hdr.Add(new Riff.Write.Chunk.RawChunk { Identifier = "IDIT", Data = new byte[43] });
                        hdr.Add(new Riff.Write.Chunk.RawChunk { Identifier = "IDIT", Data = new byte[26] });
                        using (var fs = new FileStream(@"C:\scratch\test.riff", FileMode.OpenOrCreate))
                        {
                            using (var writer = new BinaryWriter(fs, System.Text.Encoding.ASCII, false))
                            {
                                hdr.Write(writer);
                            }
                        }
                    }
                }
            }
        }
    }
}
