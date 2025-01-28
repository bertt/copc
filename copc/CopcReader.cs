namespace copc;
public static class CopcReader
{
    public static Copc Read(BinaryReader reader)
    {
        var header = LasHeaderReader.Read(reader);
        var vlrInfo = VlrInfoReader.Read(reader);
        var copcInfo = CopcInfoReader.Read(reader);
        return new Copc(header, vlrInfo, copcInfo);
    }
}
