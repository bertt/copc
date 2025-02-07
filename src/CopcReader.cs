﻿using copc.io;

namespace copc;
public static class CopcReader
{
    public static async Task<Copc> Read(string file)
    {
        var processor = new BinaryFileProcessor(new LocalBinaryFileReader(file));
        var copc = await Read(processor);
        return copc;
    }

    public static async Task<Copc> Read(HttpClient client, string url)
    {
        var processor = new BinaryFileProcessor(new HttpBinaryFileReader(client, url));
        var copc = await Read(processor);
        return copc;
    }

    private static async Task<Copc> Read(BinaryFileProcessor processor)
    {
        var headerBytes = await processor.Read(0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = Read(reader);
        var header = copc.Header;

        var vlrs = await VlrReader.GetVlrs(processor, header);
        copc.Vlrs.AddRange(vlrs);
        return copc;
    }

    private static Copc Read(BinaryReader reader)
    {
        var header = LasHeaderReader.Read(reader);
        var vlrInfo = VlrInfoReader.Read(reader);
        var copcInfo = CopcInfoReader.Read(reader);
        var copc = new Copc(header, copcInfo);
        copc.Vlrs.Add(vlrInfo);

        return copc;
    }

}
