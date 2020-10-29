using System.IO;
using Riff.Read.Chunk;

namespace Riff.Write.Chunk.Lazy
{
    public class LazyReadRawChunk : ChunkBase
    {
        private readonly RawChunkDescriptor _source;
        private readonly ISourceStreamProvider _lazyReadProvider;

        public LazyReadRawChunk(Riff.Read.Chunk.RawChunkDescriptor source, ISourceStreamProvider lazyReadProvider)
        {
            _source = source;
            _lazyReadProvider = lazyReadProvider;
        }

        public override int DataSize => _source.Size;

        public override string Identifier => _source.Identifier;

        public override int TotalSize  => RiffUtils.CalculateChunkDiskSize(DataSize);
        
        protected override void WriteData(BinaryWriter writer)
        {
            using (var sourceStream = _lazyReadProvider.Provide())
            {
                writer.WriteChunkData(_source.ReadData(sourceStream));
            }
        }
    }
}