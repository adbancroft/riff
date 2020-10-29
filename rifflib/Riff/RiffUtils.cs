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
        
        public static int CalculatePadding(int size)
        {
            const int wordSize = sizeof(short);
            return ((size + wordSize - 1) / wordSize * wordSize) - size; 
        }

        public static int CalculateChunkDiskSize(int dataSize)
        {
            return RiffUtils.HeaderSize + dataSize + RiffUtils.CalculatePadding(dataSize);
        }
    }
}