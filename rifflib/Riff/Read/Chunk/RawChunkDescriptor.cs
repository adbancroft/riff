using System.IO;

namespace Riff.Read.Chunk
{
    public class RawChunkDescriptor : ChunkDescriptorBase 
    {
        public RawChunkDescriptor(string identifier)
            : base(identifier)
        {
        }

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);
            // Callers expect the reader to point to the 1st byte
            // Note that the data is always padded to the nearest word boundary
            reader.BaseStream.Seek(Size+RiffUtils.CalculatePadding(Size), SeekOrigin.Current);
        }
    }
}