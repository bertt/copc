using copc.io;

namespace copc;
public static class VlrReader
{
    public static async Task<List<VlrInfo>> GetVlrs(BinaryFileProcessor processor, LasHeader header)
    {
        var vlrs = new List<VlrInfo>();
        var vlrCount = (int)header.VlrCount;
        var start = 589;
        for (var i = 0; i < vlrCount - 1; i++)
        {
            var end = start + (i + 1) * 54;
            var vlr = await GetVlr(processor, start, end);
            vlrs.Add(vlr);

            start = end;
            end = start + vlr.RecordLength;

            // todo read record?

            start = end;
        }

        return vlrs;
    }

    private static async Task<VlrInfo> GetVlr(BinaryFileProcessor processor, int start, int end)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrInfoReader.Read(reader);
        return vlr;
    }
}
