using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Riff.Write.Chunk.Lazy;

namespace Riff.Read.Chunk
{
    public class ListChunkDescriptor : ChunkDescriptorBase
    {
        [JsonProperty("ChildChunks", Order = 1)]
        private IList<ChunkDescriptorBase> _subChunks;

        public String ListType { get; private set; }

        public ListChunkDescriptor(string identifier)
            : base(identifier)
        {
        }

        protected override void ReadData(BinaryReader reader, IChunkFactory chunkFactory)
        {
            ListType = reader.ReadFixedString(RiffUtils.ListTypeSize);
            _subChunks = ReadSubChunks(reader, chunkFactory, Size-RiffUtils.LengthSize);
        }

        // <inheritdoc>
        public override Riff.Write.Chunk.ChunkBase CreateWriteChunk(ISourceStreamProvider provider)
        {
            return new Riff.Write.Chunk.ListChunk(this, provider);
        }

        #region IEnumerable

        public override IEnumerator<ChunkDescriptorBase> GetEnumerator()
        {
            return _subChunks.GetEnumerator();
        }
        
        #endregion

        private static IList<ChunkDescriptorBase> ReadSubChunks(BinaryReader reader, IChunkFactory chunkFactory, int expectedLength)
        {
            var endOffset = reader.BaseStream.Position+expectedLength;

            var chunks = new List<ChunkDescriptorBase>();
            while (reader.BaseStream.Position<endOffset)
            {
                ChunkDescriptorBase next = chunkFactory.Create(ChunkUtils.ReadIdentifier(reader));
                next.Read(reader, chunkFactory);
                chunks.Add(next);
                
            }
            return chunks;
        }
    }
}