namespace copc;
public static class CopcReader
{
    public static Copc Read(string file)
    {
        var headerBytes = BinaryFileReader.Read(file, 0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = Read(reader);
        // Todo: add VLRS
        return copc;
    }

    public static async Task<Copc> Read(HttpClient client, Uri uri)
    {
        var headerBytes = await BinaryFileReader.Read(client, uri, 0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = Read(reader);
        var header = copc.Header;

        var vlrs = await VlrUriReader.GetVlrs(client, uri, header);
        copc.Vlrs.AddRange(vlrs);

        return copc;
    }

    public static Copc Read(BinaryReader reader)
    {
        var header = LasHeaderReader.Read(reader);
        var vlrInfo = VlrReader.Read(reader);
        var copcInfo = CopcInfoReader.Read(reader);
        var copc = new Copc(header, copcInfo);
        copc.Vlrs.Add(vlrInfo);

        return copc;
    }
}
