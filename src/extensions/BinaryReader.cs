using Copc.Las;

namespace Copc;
public static class BinaryReaderExtensions
{
    public static Copc ReadCopc(this BinaryReader reader)
    {
        var header = LasHeaderReader.Read(reader);
        var vlrInfo = VlrReader.Read(reader);
        var copcInfo = CopcInfoReader.Read(reader);
        var copc = new Copc(header, copcInfo);
        copc.Vlrs.Add(vlrInfo);
        return copc;
    }

}
