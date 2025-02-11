using Copc.Las;

namespace Copc;

public class Copc
{
    public Copc(LasHeader header, CopcInfo copcInfo)
    {
        Vlrs = new List<Vlr>();
        Header = header;
        CopcInfo = copcInfo;
    }

    public LasHeader Header { get; set; }

    public List<Vlr> Vlrs { get; set; }

    public List<Vlr> Evlrs { get; set; }

    public CopcInfo CopcInfo { get; set; }
}
