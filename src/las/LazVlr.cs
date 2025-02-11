namespace Copc.Las;
public class LazVlr
{
    public ushort Compressor { get; set; }
    public ushort Coder { get; set; }
    public byte VersionMajor { get; set; }
    public byte VersionMinor { get; set; }
    public ushort VersionRevision { get; set; }
    public uint Options { get; set; }
    public uint ChunkSize { get; set; }
    public long EvlrsCount { get; set; }
    public long EvlrsOffset { get; set; }
    public short NumberOfItems { get; set; }
    public List<LazVlrItem> Items { get; set; } = new List<LazVlrItem>();
}
