using System.IO;
using Riff.Read.Chunk;

namespace Riff.Write.Chunk
{
    public class LazyReadRawChunk : ChunkBase
    {
        private readonly RawChunkDescriptor _source;

        public LazyReadRawChunk(Riff.Read.Chunk.RawChunkDescriptor source)
        {
            _source = source;
        }

        public override int DataSize => _source.Size;

        public override string Identifier => _source.Identifier;

        public override int TotalSize  => RiffUtils.CalculateChunkDiskSize(DataSize);
        
        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(_source.ReadData());
        }
    }
}