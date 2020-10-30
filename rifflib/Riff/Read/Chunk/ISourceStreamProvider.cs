using System.IO;

namespace Riff.Read.Chunk
{
    public interface ISourceStreamProvider 
    {
        Stream Provide();
    }

    public class FileSourceStreamProvider : ISourceStreamProvider 
    {
        private readonly string _path;

        public FileSourceStreamProvider(string path)
        {
            _path = path;
        }

        public Stream Provide()
        {
            return new FileStream(_path, FileMode.Open);
        }
    }
}