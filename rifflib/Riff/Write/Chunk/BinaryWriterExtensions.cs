using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Write.Chunk
{
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Write a fixed length ASCII string to the binary writer.
        /// </summary>
        /// <param name="writer">The reader to read from</param>
        /// <param name="toWrite">The string to write</param>
        /// <param name="size">The size of the string in bytes</param>
        public static void WriteFixedString(this BinaryWriter writer, string toWrite, int size)
        {
            toWrite = toWrite.PadRight(size).Substring(0, size);
            writer.Write(System.Text.Encoding.ASCII.GetBytes(toWrite));
        }
    }
}