﻿namespace Copc.Las;
public class VlrInfo
{
    public ushort Reserved { get; set; }
    public string UserId { get; set; }
    public ushort RecordId { get; set; }
    public ushort RecordLength { get; set; }
    public string Description { get; set; }
}
