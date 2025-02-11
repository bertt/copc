namespace Copc.Las;
public class Vlr
{
    public long ContentOffset { get; set; }
    public ushort Reserved { get; set; }
    public string UserId { get; set; }
    public ushort RecordId { get; set; }
    public ushort RecordLength { get; set; }
    public string Description { get; set; }

    public object Data { get; set; }
}
