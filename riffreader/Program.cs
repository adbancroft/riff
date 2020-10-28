using System.IO.MemoryMappedFiles;
using Riff;
using Riff.Chunk;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace riffreader
{
    class Program
    {        
        static void Main(string[] args)
        {
            string path = @"//Desktop-27m6eeq/d/Users/Bancrofts/pictures/2006/Andrews Mum & Dad\Mum in pool - 1.AVI";
            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile (path))
            {
                using (var viewStream = mmf.CreateViewStream())
                {
                    using (var reader = new BinaryReader(viewStream, System.Text.Encoding.ASCII, false))
                    {
                        var riffChunk = RiffReader.Read(reader, new BasicChunkFactory());
                        var json = JsonConvert.SerializeObject(riffChunk);
                        var streamName = riffChunk.WhereFourCc("hdrl")
                                            .First()
                                            .WhereFourCc("strl")
                                            .First()
                                            .Where(cb => cb.Identifier=="strd")
                                            .First();
                        // var idit = riffChunk.OfType<ListChunk>()
                        //                     .Where(cb=>cb.FourCC=="hdrl")
                        //                     .OfType<ListChunk>()
                        //                     .SelectMany(hdrl=>hdrl)
                        //                     .Where(cb=>cb.Identifier=="IDIT")
                        //                     .OfType<RawChunk>()
                        //                     .FirstOrDefault();
                        var value = streamName.ReadStringData(viewStream);
                    }
                }
            }
        }
    }
}
