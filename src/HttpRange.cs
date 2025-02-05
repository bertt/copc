using System.Net.Http.Headers;

namespace copc;
public static class HttpRange
{
    public static async Task<byte[]> GetHttpRange(HttpClient httpClient, string url, int start = 0, int end = 0)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new RangeHeaderValue(start, end);

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content;
    }

}
