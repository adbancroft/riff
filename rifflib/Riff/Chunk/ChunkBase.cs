using System;
using System.Collections.Generic;
using System.IO;
using Validation;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Riff.Chunk
{
    [JsonObject]
    public abstract class ChunkBase : IEnumerable<ChunkBase>
    {
        public const int IdentifierSize = 4;
        public const int LengthSize = 4;
        public const int HeaderSize = IdentifierSize + LengthSize;

        protected ChunkBase(string identifier)
        {
            Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));
            Requires.Argument(identifier.Length==4, nameof(identifier), "Invalid identifier: "+ identifier);

            Identifier = identifier;
        }

        public virtual void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            ChunkOffset = reader.BaseStream.Position-IdentifierSize;
            Length = reader.ReadInt32();
        }

        public virtual byte[] ReadData(Stream source)
        {
            source.Seek(ChunkOffset + HeaderSize, SeekOrigin.Begin);
            using (var reader = new BinaryReader(source, System.Text.Encoding.ASCII, true))
            {
                return reader.ReadBytes(Length);
            }
        }

        public long ChunkOffset { get; private set; }

        public String Identifier { get; set; }

        public int Length { get; private set; }

        public int Padding
        {
            get
            {
                var wordSize = sizeof(short);
                return ((Length + wordSize - 1) / wordSize * wordSize) - Length;
            }
        }

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        virtual public IEnumerator<ChunkBase> GetEnumerator()
        {
            return Enumerable.Empty<ChunkBase>().GetEnumerator();
        }

        #endregion
    }
}