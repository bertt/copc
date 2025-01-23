using NUnit.Framework;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace copc;

public class Tests
{
    [Test]
    public async Task ReadCOPC()
    {

        // 589 bytes
        // 2.3GB sofi stadium
        var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";
        var httpClient = new HttpClient();
        var headerBytes = await GetHttpRange(url, httpClient, 0, 589);

        var header = HeaderParser.ParseHeader(headerBytes);
        Assert.That(header.FileSignature == "LASF");
        Assert.That(header.FileSourceId == 0);
        Assert.That(header.GlobalEncoding == 16);
        Assert.That(header.ProjectId == Guid.Empty);
        Assert.That(header.LasMajorVersion == 1);
        Assert.That(header.LasMinorVersion == 4);

        Assert.That(header.CopcSignature == "copc");
        Assert.That(header.MajorVersion == 1);
        Assert.That(header.MinorVersion == 0);
        Assert.That(header.PointDataRecordFormat == 6);
        Assert.That(header.PointDataRecordLength == 30);


        // points should be 364,384,576
    }

    private static async Task<byte[]> GetHttpRange(string url, HttpClient httpClient, int start = 0, int end = 0)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new RangeHeaderValue(start, end);

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content;
    }
}