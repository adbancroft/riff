using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// A writable chunk that stores child chunks
    /// </summary>
    public class ListChunk : ChunkBase, IListChunk
    {
        private readonly IList<ChunkBase> _subChunks = new List<ChunkBase>();

        /// <summary>
        /// Default construction
        /// </summary>
        public ListChunk()
        {
        }

        /// <summary>
        /// Construct from a list chunk descriptor
        /// </summary>
        /// <param name="source">The list chunk descriptor</param>
        public ListChunk(Riff.Read.Chunk.ListChunkDescriptor source)
        {
            Identifier = source.Identifier;
            ListType = source.ListType;
            foreach (var descriptor in source)
            {
                _subChunks.Add(descriptor.CreateWriteChunk());
            }
        }

        /// <summary>
        /// The list type. E.g. "hdrl"
        /// </summary>
        /// <value></value>
        public String ListType { get; set; }

        /// <inheritdoc/>
        public override int DataSize => RiffUtils.ListTypeSize + _subChunks.Sum(s => s.TotalSize);

        /// <inheritdoc/>
        public override int TotalSize => RiffUtils.HeaderSize + DataSize;

        /// <inheritdoc/>
        public override void WriteData(BinaryWriter writer)
        {
            // Write the chunk and it's child chunks
            writer.WriteFixedString(ListType, RiffUtils.ListTypeSize);
            foreach (var item in _subChunks)
            {
                item.Write(writer);
            }
        }

        #region IList<>

        /// <inheritdoc/>
        public override ChunkBase this[int index]
        {
            get { return _subChunks[index]; }
            set { _subChunks[index] = value; }
        }

        /// <inheritdoc/>
        public override int IndexOf(ChunkBase item) { return _subChunks.IndexOf(item); }

        /// <inheritdoc/>
        public override void Insert(int index, ChunkBase item) { _subChunks.Insert(index, item); }

        /// <inheritdoc/>
        public override void RemoveAt(int index) { _subChunks.RemoveAt(index); }

        #endregion

        #region ICollection<>

        /// <inheritdoc/>
        public override int Count { get { return _subChunks.Count; } }

        /// <inheritdoc/>
        public override bool IsReadOnly { get { return _subChunks.IsReadOnly; } }

        /// <inheritdoc/>
        public override void Add(ChunkBase item)  { _subChunks.Add(item); }

        /// <inheritdoc/>
        public override void Clear()  { _subChunks.Clear(); }

        /// <inheritdoc/>
        public override bool Contains(ChunkBase item) { return _subChunks.Contains(item); }

        /// <inheritdoc/>
        public override void CopyTo(ChunkBase[] array, int arrayIndex)  { _subChunks.CopyTo(array, arrayIndex); }

        /// <inheritdoc/>
        public override bool Remove(ChunkBase item)  { return _subChunks.Remove(item); }

        #endregion

        #region IEnumerable

        /// <inheritdoc/>
        public override IEnumerator<ChunkBase> GetEnumerator()
        {
            return _subChunks.GetEnumerator();
        }

        #endregion
    }
}