using System.IO;

namespace Riff.Write.Chunk
{
    public class RawChunk : ChunkBase
    {
        public virtual byte[] Data { get; set; }

        public override int DataSize { get { return Data?.Length ?? 0; } }

        public RawChunk() 
        {
        }
        public RawChunk(Riff.Read.Chunk.RawChunkDescriptor source)
        {
            Identifier = source.Identifier;
            Data = new byte[12];
            //_source = source;
        }

        protected override void WriteData(BinaryWriter writer)
        {
            writer.Write(Data ?? new byte[0]);

            // The data is always padded to the nearest word boundary
            int padding = RiffUtils.CalculatePadding(Data?.Length ?? 0);
            while (padding!=0)
            {
                writer.Write(new byte());
                --padding;
            }
        }        
    }
}