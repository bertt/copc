namespace copc;

public class BinaryFileReader
{
    public static async Task<byte[]> Read(string filePath, long start, long end)
    {
        long length = end - start + 1;
        byte[] buffer = new byte[length];

        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            fs.Seek(start, SeekOrigin.Begin);
            await fs.ReadAsync(buffer, 0, buffer.Length);
        }

        return buffer;
    }
    public static async Task<byte[]> Read(Uri uri, long start, long end)
    {
        var client = new HttpClient();
        return await Read(client, uri, start, end);
    }

    public static async Task<byte[]> Read(HttpClient client, Uri uri, long start, long end)
    {
        client.DefaultRequestHeaders.Range = new System.Net.Http.Headers.RangeHeaderValue(start, end);
        var response = await client.GetAsync(uri.AbsoluteUri, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsByteArrayAsync();
    }
}