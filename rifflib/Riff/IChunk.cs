namespace Riff
{
    /// <summary>
    /// Common chunk attributes and behavior
    /// </summary>
    public interface IChunk
    {
        /// <summary>
        /// The 4 character chunk identifier. E.g. LIST
        /// </summary>
        string Identifier { get; }
    }

    /// <summary>
    /// Common list chunk attributes and behavior
    /// </summary>
    public interface IListChunk : IChunk
    {
        /// <summary>
        /// The list type FourCC tag
        /// </summary>
        string ListType { get; }
    }
}