using System.IO;
using Riff.Read;
using Riff.Read.Chunk;

namespace rifftool
{
    internal static class FileInfoExtensions
    {
        public static ChunkDescriptorBase ReadRiff(this FileInfo input)
        {
            using var reader = new BinaryReader(new FileStream(input.FullName, FileMode.Open));
            return Reader.Read(reader, new LazyBasicChunkFactory(reader, new FileStreamProvider(input.FullName)));
        }
    }
}