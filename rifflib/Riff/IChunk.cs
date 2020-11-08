namespace Riff
{
    interface IChunk
    {
        string Identifier { get; }
    }

    interface IListChunk : IChunk
    {
        string ListType { get; }
    }
}