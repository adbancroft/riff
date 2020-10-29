using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);
            ListType = reader.ReadFixedString(RiffUtils.ListTypeSize);
            _subChunks = ReadSubChunks(reader, chunkFactory, Size-LengthSize);
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