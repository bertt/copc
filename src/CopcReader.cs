using Copc;
using Copc.Io;

namespace copc;
public static class CopcReader
{
    public static async Task<Copc.Copc> Read(string file)
    {
        var processor = new LocalBinaryFileReader(file);
        var copc = await processor.Read();
        return copc;
    }

    public static async Task<Copc.Copc> Read(HttpClient client, string url)
    {
        var processor = new HttpBinaryFileReader(client, url);
        var copc = await processor.Read();
        return copc;
    }
}
