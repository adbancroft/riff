using System.IO;
using Riff.Read.Chunk;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Encapsulates a chunk with a byte[] data payload
    /// </summary>
    public class ByteArrayChunk : ChunkBase
    {
        /// <summary>
        /// Default constructor. Caller will initialize via proprty setters
        /// </summary>
        public ByteArrayChunk()
        {
        }

        /// <summary>
        /// Construct from a chunk descriptor
        /// </summary>
        /// <param name="source">Source to initalize from</param>
        public ByteArrayChunk(ChunkDescriptorBase source)
        {
            Identifier = source.Identifier;
            Data = source.Data;
        }

        /// <summary>
        /// The chunk payload
        /// </summary>
        public byte[] Data { get; set; }

        /// <inheritdoc/>
        public override int DataSize => Data?.Length ?? 0;

        /// <inheritdoc/>
        public override int TotalSize => RiffUtils.CalculateTotalChunkSIze(DataSize);

        /// <inheritdoc/>
        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }        
    }
}