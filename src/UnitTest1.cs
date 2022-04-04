using NUnit.Framework;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace copc;

public class Tests
{
    [Test]
    public async Task ReadCOPC()
    {
        // 2.3GB sofi stadium
        var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";

        // specs https://copc.io/
        var httpClient = new HttpClient();
        string lasfHeader= await GetHttpRange(httpClient, url, 0, 3);
        Assert.IsTrue(lasfHeader == "LASF");

        string copcHeader = await GetHttpRange(httpClient, url, 377, 380);
        Assert.IsTrue(copcHeader == "copc");


        var copcVersion = await GetHttpRangeBytes(url, httpClient, 393, 394);

        Assert.IsTrue(copcVersion[0] == 1);
        Assert.IsTrue(copcVersion[1] == 0);

    }

    private static async Task<string> GetHttpRange(HttpClient client, string url, int start, int end)
    {
        var timer = new Stopwatch();
        timer.Start();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new RangeHeaderValue(start, end);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        timer.Stop();
        Debug.WriteLine($"timer {timer.ElapsedMilliseconds}ms");

        return content;
    }

    private static async Task<byte[]> GetHttpRangeBytes(string url, HttpClient httpClient, int start = 0, int end = 0)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new RangeHeaderValue(start, end);

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content;
    }
}