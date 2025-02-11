using Copc.Las;

namespace Copc;

public class Copc
{
    public Copc(LasHeader header)
    {
        Vlrs = new List<Vlr>();
        Header = header;
    }

    public LasHeader Header { get; set; }

    public List<Vlr> Vlrs { get; set; }
}
