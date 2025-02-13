using copc.copc;
using Copc.Las;

namespace Copc;

public class Copc
{
    public Copc(LasHeader header)
    {
        Vlrs = new List<Vlr>();
        Header = header;
        HierarchyPages = new List<HierarchyPage>();
    }

    public LasHeader Header { get; set; }

    public List<Vlr> Vlrs { get; set; }

    public List<HierarchyPage> HierarchyPages { get; set; }
}
