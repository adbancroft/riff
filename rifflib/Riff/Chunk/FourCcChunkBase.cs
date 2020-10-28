using System;
using System.IO;

namespace Riff.Chunk
{
    public abstract class FourCcChunkBase : ChunkBase 
    {
        public const int FourCCSize = 4;

        public String FourCC { get; set; }

        protected FourCcChunkBase(string identifier)
            : base(identifier)
        {
        }

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);            
            FourCC = reader.ReadFixedString(FourCCSize);
        }
    }
}