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
    /// Base class for all chunks.
    /// </summary>
    /// <remarks>
    /// Chunks can contain other chunks, in a tree structure.
    /// </remarks>
    [JsonObject]
    public abstract class ChunkDescriptorBase : IEnumerable<ChunkDescriptorBase>
    {        


        /// <summary>
        /// Construct a new chunk
        /// </summary>
        /// <param name="identifier">The 4 character chunk identifier</param>
        protected ChunkDescriptorBase(string identifier)
        {
            Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));
            Requires.Argument(identifier.Length==4, nameof(identifier), "Invalid identifier: "+ identifier);

            Identifier = identifier;
        }

        /// <summary>
        /// Read this chunk's header fields from the reader
        /// </summary>
        /// <param name="reader">The reader to read from</param>
        /// <param name="chunkFactory">Used by derived classes to create new child chunks</param>
        public virtual void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            ChunkOffset = reader.BaseStream.Position-RiffUtils.IdentifierSize;
            Size = reader.ReadInt32();
        }

        /// <summary>
        /// The offset of the start of this chunk from the beginning of the input stream 
        /// </summary>
        public long ChunkOffset { get; private set; }

        /// <summary>
        /// The 4 character chunk identifier. E.g. LIST
        /// </summary>
        /// <value></value>
        public String Identifier { get; private set; }

        /// <summary>
        /// Size of the chunk data in bytes.
        /// </summary>
        /// <remarks>
        /// Size gives the size of the valid data in the chunk; it does not include the padding, the size of the identifier, or the size of the size field itself.
        /// </remarks>
        public int Size { get; private set; }

        /// <summary>
        /// Create the corresponding write chunk
        /// </summary>
        /// <returns></returns>
        public abstract Riff.Write.Chunk.ChunkBase CreateWriteChunk();

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        virtual public IEnumerator<ChunkDescriptorBase> GetEnumerator()
        {
            return Enumerable.Empty<ChunkDescriptorBase>().GetEnumerator();
        }

        #endregion
    }
}