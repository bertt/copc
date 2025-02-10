using Copc.Io;
using Copc.Las;

namespace Copc;
public static class BinaryFileReaderExtensions
{
    public static async Task<Copc> Read(this BinaryFileReader binaryFileReader)
    {
        var headerBytes = await binaryFileReader.Read(0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = reader.ReadCopc();
        var header = copc.Header;
        var vlrCount = (int)header.VlrCount;
        var vlrs = await binaryFileReader.GetVlrs(vlrCount);
        copc.Vlrs.AddRange(vlrs);
        return copc;
    }

    private static async Task<List<VlrInfo>> GetVlrs(this BinaryFileReader binaryFileReader, int vlrCount)
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

    private static async Task<VlrInfo> GetVlr(this BinaryFileReader processor, int start, int end)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrInfoReader.Read(reader);
        return vlr;
    }

}
