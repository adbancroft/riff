namespace Riff
{
    public static class RiffUtils
    {
        /// <summary>
        /// Size of the identifier field in bytes.
        /// </summary>
        public const int IdentifierSize = 4;

        /// <summary>
        /// Size of the length field in bytes.
        /// </summary>
        public const int LengthSize = 4;

        /// <summary>
        /// Size of the list type field in bytes
        /// </summary>
        public const int ListTypeSize = 4;

        /// <summary>
        /// Size of the fixed chunk header fields in bytes.
        /// </summary>
        public const int HeaderSize = IdentifierSize + LengthSize;
        
        /// <summary>
        /// RIFF chunks are WORD aligned, so compute the necessary padding
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int CalculatePadding(int size)
        {
            const int wordSize = sizeof(short);
            return ((size + wordSize - 1) / wordSize * wordSize) - size; 
        }

        /// <summary>
        /// Calculate total chunk size from start of the chunk identifier to 
        /// the end of the payload, including padding.
        /// </summary>
        /// <param name="dataSize">Chunk data size</param>
        public static int CalculateTotalChunkSIze(int dataSize)
        {
            return RiffUtils.HeaderSize + dataSize + RiffUtils.CalculatePadding(dataSize);
        }
    }
}