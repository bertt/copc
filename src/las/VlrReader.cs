using copc.io;

namespace copc.las;
public static class VlrReader
{
    public static async Task<List<VlrInfo>> GetVlrs(BinaryFileReader binaryFileReader, int vlrCount)
    {
        var vlrs = new List<VlrInfo>();
        var start = 589;
        for (var i = 0; i < vlrCount - 1; i++)
        {
            var end = start + (i + 1) * 54;
            var vlr = await GetVlr(binaryFileReader, start, end);
            vlrs.Add(vlr);

            start = end;
            end = start + vlr.RecordLength;

            // todo read record?

            start = end;
        }

        return vlrs;
    }

    private static async Task<VlrInfo> GetVlr(BinaryFileReader processor, int start, int end)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrInfoReader.Read(reader);
        return vlr;
    }
}
