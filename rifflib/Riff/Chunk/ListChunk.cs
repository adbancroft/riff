using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Riff.Chunk
{
    public class ListChunk : FourCcChunkBase
    {
        [JsonProperty("ChildChunks", Order = 1)]
        private IList<ChunkBase> _subChunks;

        public ListChunk(string identifier)
            : base(identifier)
        {
        }

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);

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