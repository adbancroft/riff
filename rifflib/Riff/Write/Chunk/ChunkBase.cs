using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Base class for all writeable chunks.
    /// </summary>
    /// <remarks>
    /// Chunks can contain other chunks, in a tree structure.
    /// </remarks>
    public abstract class ChunkBase : IList<ChunkBase>
    {
        /// <summary>
        /// The 4 character chunk identifier. E.g. LIST
        /// </summary>
        public virtual String Identifier { get; set; }

        /// <summary>
        /// Size of the chunk data. 
        /// Does not include the size of the <cref see="Identifier"/> or this field
        /// </summary>
        public abstract int DataSize { get; }

        /// <summary>
        /// Size of the chunk from end to end, including padding.
        /// </summary>
        public abstract int TotalSize { get ; }

        /// <summary>
        /// Write this chunk.
        /// </summary>
        /// <param name="writer"></param>
        public void Write(BinaryWriter writer)
        {
            writer.WriteFixedString(Identifier, RiffUtils.IdentifierSize);
            writer.Write(DataSize);

            // Use Template Method pattern to write data
            WriteData(writer);
        }

        /// <summary>
        /// Derived classes override to write the data payload.
        /// </summary>
        /// <param name="writer">Write the data to this</param>
        public abstract void WriteData(BinaryWriter writer);
        
        #region IList<>

        /// <inheritdoc/>
        public virtual ChunkBase this[int index] 
        { 
            get { throw new NotImplementedException(); } 
            set { throw new NotImplementedException(); } 
        }

        /// <inheritdoc/>
        public virtual int IndexOf(ChunkBase item) { throw new NotImplementedException(); } 

        /// <inheritdoc/>
        public virtual void Insert(int index, ChunkBase item) { throw new NotImplementedException(); } 

        /// <inheritdoc/>
        public virtual void RemoveAt(int index) { throw new NotImplementedException(); } 
        
        #endregion

        #region ICollection<>

        /// <inheritdoc/>
        public virtual int Count { get { return 0; } }

        /// <inheritdoc/>
        public virtual bool IsReadOnly { get { return true; } }


        /// <inheritdoc/>
        public virtual void Add(ChunkBase item)  { throw new NotImplementedException(); } 

        /// <inheritdoc/>
        public virtual void Clear()  { throw new NotImplementedException(); } 

        /// <inheritdoc/>
        public virtual bool Contains(ChunkBase item) { return false; }

        /// <inheritdoc/>
        public virtual void CopyTo(ChunkBase[] array, int arrayIndex)  { throw new NotImplementedException(); } 

        /// <inheritdoc/>
        public virtual bool Remove(ChunkBase item)  { throw new NotImplementedException(); } 

        #endregion

        #region IEnumerable

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        virtual public IEnumerator<ChunkBase> GetEnumerator()
        {
            return Enumerable.Empty<ChunkBase>().GetEnumerator();
        }

        #endregion
    }
}