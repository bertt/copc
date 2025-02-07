namespace copc;
public static class VlrUriReader
{
    public static async Task<List<VlrInfo>> GetVlrs(HttpClient client, Uri uri, LasHeader header)
    {
        var vlrs = new List<VlrInfo>();
        var vlrCount = (int)header.VlrCount;
        var start = 589;
        for (var i = 0; i < vlrCount - 1; i++)
        {
            var end = start + (i + 1) * 54;
            var vlr = await GetVlrFromUrl(client, uri, start, end);
            vlrs.Add(vlr);

            start = end;
            end = start + vlr.RecordLength;

            // todo read record?

            start = end;
        }

        return vlrs;
    }

    private static async Task<VlrInfo> GetVlrFromUrl(HttpClient client, Uri uri, int start, int end)
    {
        var headerBytes1 = await BinaryFileReader.Read(client, uri, start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrReader.Read(reader);
        return vlr;
    }
}
