using System.IO;
using System.Management.Automation;  // Windows PowerShell assembly.
using Riff.Read;
using Riff.Read.Chunk;

namespace Riff
{
  /// <summary>
  /// <para type="synopsis">Read a RIFF file</para>
  /// <para type="description">Reads a resource interchange file format (RIFF) file into a collection of chunks</para>
  /// <para type="description">Common uses of the RIFF format are multimedia containers such as AVI and WAV files.</para>
  /// <para type="link" uri="https://github.com/adbancroft/riff/">Source code</para>
  /// </summary>
  /// <example>
  ///   <code>$chunks = Read-Riff -Path &quot;C:/test.avi&quot;</code>
  /// </example>
  [Cmdlet(VerbsCommunications.Read, "Riff")]
  [OutputType(typeof(ChunkDescriptorBase))]
  public class ReadRiffCommand : Cmdlet
  {
    /// <summary>
    /// The RIFF file path
    /// </summary>
    [Parameter(Mandatory=true, ValueFromPipeline=true)]
    public string Path { get; set;}

    /// <summary>
    /// Read the RIFF file
    /// </summary>
    protected override void ProcessRecord()
    {
      using var reader = new BinaryReader(new FileStream(Path, FileMode.Open));
      WriteObject(Reader.Read(reader, new LazyBasicChunkFactory(reader, new FileStreamProvider(Path))));
    }
  }
}