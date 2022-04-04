using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace copc;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        // sofi stadium
        var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";

        // format https://copc.io/
        var httpClient = new HttpClient();
        string lasfHeader= await GetHttpRange(url, httpClient, 0, 3);
        Assert.IsTrue(lasfHeader == "LASF");

        string copcHeader = await GetHttpRange(url, httpClient, 377, 380);
        Assert.IsTrue(copcHeader == "copc");

        var copcVersion = await GetHttpRangeBytes(url, httpClient, 393, 394);
        
        Assert.IsTrue(copcVersion[0] == 1);
        Assert.IsTrue(copcVersion[1] == 0);
    }

    private static async Task<string> GetHttpRange(string url, HttpClient httpClient, int start = 0, int end = 0)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new RangeHeaderValue(start, end);

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

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