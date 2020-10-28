using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Riff.Read.Chunk
{
    public class ListChunk : ChunkBase
    {
        [JsonProperty("ChildChunks", Order = 1)]
        private IList<ChunkBase> _subChunks;

        public const int ListTypeSize = 4;

        public String ListType { get; private set; }

        public ListChunk(string identifier)
            : base(identifier)
        {
        }

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);
            ListType = reader.ReadFixedString(ListTypeSize);
            _subChunks = ReadSubChunks(reader, chunkFactory, Size-LengthSize);
        }

        #region IEnumerable

        public override IEnumerator<ChunkBase> GetEnumerator()
        {
            return _subChunks.GetEnumerator();
        }
        
        #endregion

        private static IList<ChunkBase> ReadSubChunks(BinaryReader reader, IChunkFactory chunkFactory, int expectedLength)
        {
            var endOffset = reader.BaseStream.Position+expectedLength;

            var chunks = new List<ChunkBase>();
            while (reader.BaseStream.Position<endOffset)
            {
                ChunkBase next = chunkFactory.Create(ChunkUtils.ReadIdentifier(reader));
                next.Read(reader, chunkFactory);
                chunks.Add(next);
                
            }
            return chunks;
        }
    }
}