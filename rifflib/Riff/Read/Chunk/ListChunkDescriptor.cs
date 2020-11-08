using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Validation;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// A chunk descriptor that has child chunks.
    /// </summary>
    public class ListChunkDescriptor : ChunkDescriptorBase
    {
        [JsonProperty("ChildChunks", Order = 1)]
        private readonly IList<ChunkDescriptorBase> _subChunks;

        /// <summary>
        /// The list type FourCC tag
        /// </summary>
        public String ListType { get; }

        /// <inheritdoc/>
        public override byte[] Data { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Construct by reading from a BinaryReader
        /// </summary>
        /// <param name="identifier">The chunk identifer</param>
        /// <param name="reader">The source to read from</param>
        /// <param name="chunkFactory">Used to create child chunks</param>
        public ListChunkDescriptor(string identifier, BinaryReader reader, IChunkFactory chunkFactory)
            : base(identifier, reader)
        {
            Requires.NotNull(chunkFactory, nameof(chunkFactory));

            ListType = reader.ReadFixedString(RiffUtils.ListTypeSize);
            Assumes.NotNullOrEmpty(ListType);
            Assumes.True(ListType.Length==4, "Invalid list type: "+ ListType);

            _subChunks = ReadSubChunks(reader, chunkFactory, Size-RiffUtils.ListTypeSize);
        }

        /// <inheritdoc/>
        public override Write.Chunk.ChunkBase CreateWriteChunk()
        {
            return new Write.Chunk.ListChunk(this);
        }

        #region IEnumerable

        /// <inheritdoc/>
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
                chunks.Add(chunkFactory.Create(reader.ReadIdentifier()));
            }

            return chunks;
        }
    }
}