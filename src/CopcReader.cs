namespace copc;
public static class CopcReader
{
    public static async Task<Copc> ReadFromUrl(HttpClient client, string url)
    {
        var headerBytes = await HttpRange.GetHttpRange(client, url, 0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = Read(reader);

        var vlrs = new List<VlrInfo>();
        // get the current position
        var vlrCount = (int)copc.Header.VlrCount;
        var start = 589;
        for (var i = 0; i < vlrCount - 1; i++)
        {
            var end = start + (i + 1) * 54;
            var vlr = await GetVlrFromUrl(client, url, start, end);
            vlrs.Add(vlr);

            start = end;
            end = start + vlr.RecordLength;

            // todo read record?

            start = end;
        }
        copc.Vlrs.AddRange(vlrs);

        return copc;
    }

    private static async Task<VlrInfo> GetVlrFromUrl(HttpClient client, string url, int start, int end)
    {
        var headerBytes1 = await HttpRange.GetHttpRange(client, url, start, end);
        var reader1 = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrInfoReader.Read(reader1);
        return vlr;
    }

    public static Copc Read(BinaryReader reader)
    {
        var header = LasHeaderReader.Read(reader);
        var vlrInfo = VlrInfoReader.Read(reader);
        var copcInfo = CopcInfoReader.Read(reader);
        var copc = new Copc(header, copcInfo);
        copc.Vlrs.Add(vlrInfo);

        return copc;
    }
}
