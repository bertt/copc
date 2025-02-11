namespace Copc.Las;
public class Vlr
{
    public bool IsExtended { get; set; }
    public long ContentOffset { get; set; }
    public ushort Reserved { get; set; }
    public string UserId { get; set; }
    public ushort RecordId { get; set; }
    public long RecordLength { get; set; }
    public string Description { get; set; }
    public object Data { get; set; }
}
