using Validation;

namespace Riff.Chunk
{
    /// <summary>
    /// Top level chunk that captures the entire RIFF file
    /// </summary>
    public class RiffChunk : ListChunk
    {
        public RiffChunk(string identifier) : base(identifier)
        {
            Requires.Argument(identifier.ToLowerInvariant()=="riff", nameof(identifier), "Invalid identifier: "+ identifier);
        }
    }
}