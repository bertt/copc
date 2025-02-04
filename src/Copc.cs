namespace copc;

public class Copc
{
    public Copc(LasHeader header, VlrInfo vlrInfo, CopcInfo copcInfo)
    {
        Header = header;
        VlrInfo = vlrInfo;
        CopcInfo = copcInfo;
    }

    public LasHeader Header { get; set; }

    public VlrInfo VlrInfo { get; set; }

    public CopcInfo CopcInfo { get; set; }
}
