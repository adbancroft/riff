using System.IO;

namespace Riff.Write.Chunk
{
    public class RawChunk : ChunkBase
    {
        public byte[] Data { get; set; }

        public override int Size { get { return Data?.Length ?? 0; } }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(Data);

            // The data is always padded to the nearest word boundary
            int padding = RiffUtils.CalculatePadding(Data.Length);
            while (padding!=0)
            {
                writer.Write(new byte(0));
                --padding;
            }
        }
        
    }
}