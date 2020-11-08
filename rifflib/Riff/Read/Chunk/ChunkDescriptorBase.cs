using System;
using System.Collections.Generic;
using System.IO;
using Validation;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// Base class for all chunk descriptors read from a RIFF file.
    /// </summary>
    /// <remarks>
    /// Chunk descriptors are immutable and initialized by reading from a BinaryReader on construction
    /// Chunks can contain other chunks, in a tree structure.
    /// </remarks>
    [JsonObject]
    public abstract class ChunkDescriptorBase : IEnumerable<ChunkDescriptorBase>, IChunk
    {
        /// <summary>
        /// Construct by reading from a BinaryReader
        /// </summary>
        /// <param name="identifier">The chunk identifer</param>
        /// <param name="reader">The source to read from</param>
        protected ChunkDescriptorBase(string identifier, BinaryReader reader)
        {
            Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));
            Requires.Argument(identifier.Length==4, nameof(identifier), "Invalid identifier: "+ identifier);
            Requires.NotNull(reader, nameof(reader));

            Identifier = identifier;
            ChunkOffset = reader.BaseStream.Position-RiffUtils.IdentifierSize;
            Size = reader.ReadInt32();
        }

        /// <summary>
        /// The offset of the start of this chunk from the beginning of the input stream
        /// </summary>
        public long ChunkOffset { get; }

        /// <summary>
        /// The 4 character chunk identifier. E.g. LIST
        /// </summary>
        /// <value></value>
        public String Identifier { get; }

        /// <summary>
        /// Size of the chunk data in bytes.
        /// </summary>
        /// <remarks>
        /// Size gives the size of the valid data in the chunk; it does not include the padding, the size of the identifier, or the size of the size field itself.
        /// </remarks>
        public int Size { get; }

        /// <summary>
        /// Get the chunk data
        /// </summary>
        [JsonIgnore]
        public abstract byte[] Data { get;}

        /// <summary>
        /// Create the corresponding write chunk
        /// </summary>
        public abstract Write.Chunk.ChunkBase CreateWriteChunk();

        #region IEnumerable

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        virtual public IEnumerator<ChunkDescriptorBase> GetEnumerator()
        {
            return Enumerable.Empty<ChunkDescriptorBase>().GetEnumerator();
        }

        #endregion
    }
}