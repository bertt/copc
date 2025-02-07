using copc.las;

namespace copc.copc;

public class Copc
{
    public Copc(LasHeader header, CopcInfo copcInfo)
    {
        Vlrs = new List<VlrInfo>();
        Header = header;
        CopcInfo = copcInfo;
    }

    public LasHeader Header { get; set; }

    public List<VlrInfo> Vlrs { get; set; }

    public CopcInfo CopcInfo { get; set; }
}
