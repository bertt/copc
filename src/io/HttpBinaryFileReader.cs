using System.Net.Http.Headers;

namespace Copc.Io;

public class HttpBinaryFileReader : BinaryFileReader
{
    private readonly HttpClient client;
    public HttpBinaryFileReader(HttpClient client, string source) : base(source) => this.client = client;
    public override async Task<byte[]> Read(long start, long end)
    {
        client.DefaultRequestHeaders.Range = new RangeHeaderValue(start, end);
        var response = await client.GetAsync(source, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsByteArrayAsync();
    }
}
