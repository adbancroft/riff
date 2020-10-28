using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Chunk
{
    public static class ChunkExtensions
    {
        public static string ReadStringData(this ChunkBase chunk, Stream source)
        {
           return System.Text.Encoding.ASCII.GetString(chunk.ReadData(source)); 
        }       

        public static IEnumerable<ChunkBase> WhereFourCc(this IEnumerable<ChunkBase> source, string fourCc)
        {
            return source.OfType<FourCcChunkBase>().Where(cb => cb.FourCC==fourCc);
        }
    }
}