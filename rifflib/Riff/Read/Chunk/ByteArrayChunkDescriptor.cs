using System.IO;
using Riff.Write.Chunk;
using Newtonsoft.Json;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// A chunk that has a byte[] payload.
    /// </summary>
    public class ByteArrayChunkDescriptor : ChunkDescriptorBase 
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public override byte[] Data { get; }

        /// <summary>
        /// Construct by reading from a BinaryReader
        /// </summary>
        /// <param name="identifier">The chunk identifer</param>
        /// <param name="reader">The source to read from</param>
        public ByteArrayChunkDescriptor(string identifier, BinaryReader reader)
            : base(identifier, reader)
        {
            Data = reader.ReadBytes(Size);
            reader.BaseStream.Seek(RiffUtils.CalculatePadding(Size), SeekOrigin.Current);
        }
   
        /// <inheritdoc/>
        public override Riff.Write.Chunk.ChunkBase CreateWriteChunk()
        {
            return new ByteDescriptorChunk(this);
        }
    }
}