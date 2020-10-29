using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Write.Chunk
{
    public class ListChunk : ChunkBase
    {
        private readonly IList<ChunkBase> _subChunks = new List<ChunkBase>();

        public String ListType { get; set; }

         public override int Size 
         {
            get 
            {
                return RiffUtils.ListTypeSize + _subChunks.Sum(s => s.Size);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.WriteFixedString(ListType, RiffUtils.IdentifierSize);
            foreach (var item in _subChunks)
            {
                item.Write(writer);
            }
        }

        #region IList<>
        public override ChunkBase this[int index] 
        { 
            get { return _subChunks[index]; } 
            set { _subChunks[index] = value; } 
        }

        public override int IndexOf(ChunkBase item) { return _subChunks.IndexOf(item); } 
        public override void Insert(int index, ChunkBase item) { _subChunks.Insert(index, item); } 
        public override void RemoveAt(int index) { _subChunks.RemoveAt(index); } 
        
        #endregion

        #region ICollection<>
        public override int Count { get { return _subChunks.Count; } }
        public override bool IsReadOnly { get { return _subChunks.IsReadOnly; } }

        public override void Add(ChunkBase item)  { _subChunks.Add(item); } 
        public override void Clear()  { _subChunks.Clear(); } 
        public override bool Contains(ChunkBase item) { return _subChunks.Contains(item); }
        public override void CopyTo(ChunkBase[] array, int arrayIndex)  { _subChunks.CopyTo(array, arrayIndex); } 
        public override bool Remove(ChunkBase item)  { return _subChunks.Remove(item); } 

        #endregion

        #region IEnumerable

        public override IEnumerator<ChunkBase> GetEnumerator()
        {
            return _subChunks.GetEnumerator();
        }

        #endregion
    }
}