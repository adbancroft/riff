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
            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile (path))
            {
                using (var viewStream = mmf.CreateViewStream())
                {
                    using (var reader = new BinaryReader(viewStream, System.Text.Encoding.ASCII, false))
                    {
                        var riffChunk = Riff.Read.Reader.Read(reader, new Riff.Read.BasicChunkFactory());
                        System.Console.Write(JsonConvert.SerializeObject(riffChunk));
                    }
                }
            }
        }
    }
}
