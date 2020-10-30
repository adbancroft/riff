using System.IO;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// RIFF file chunk data can be very large, so it is lazy loaded. However, storing a Stream or Reader object in each chunk
    /// will require all chunk classes to implement IDisposable. That is a large burden on implementers and consumers.
    /// 
    /// Instead we use instances of this interface to proxy the source RIFF file stream and provide it on demand.
    /// </summary>
    public interface IStreamProvider 
    {
        /// <summary>
        /// Create the stream.
        /// 1. Calls to <see cref="Provide"> should be stable. 
        ///     I.e. repeated calls should return a stream that always wraps the same data source
        /// 2. The caller will own and dispose of the Stream.
        /// </summary>
        Stream Provide();
    }

    /// <summary>
    /// A simple IStreamProvider that wraps a disk file.
    /// </summary>
    public class FileStreamProvider : IStreamProvider 
    {
        private readonly string _path;

        public FileStreamProvider(string path)
        {
            _path = path;
        }

        /// <inheritdoc>
        public Stream Provide()
        {
            return new FileStream(_path, FileMode.Open);
        }
    }
}