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
        Assert.That(header.SystemIdentifier == "");
        Assert.That(header.GeneratingSoftware == "");
        Assert.That(header.FileCreationDayOfYear == 1);
        Assert.That(header.FileCreationYear == 1);
        Assert.That(header.HeaderSize == 375);
        Assert.That(header.PointDataOffset == 1545);
        Assert.That(header.NumberOfVariableLengthRecords == 3);
        Assert.That(header.PointDataRecordFormat == 6);
        Assert.That(header.PointDataRecordLength == 30);
        Assert.That(header.LegacyNumberOfPointRecords == 364384576);
        Assert.That(header.LegacyNumberOfPointByReturn[0] == 347247515);
        Assert.That(header.LegacyNumberOfPointByReturn[1] == 15507300);
        Assert.That(header.LegacyNumberOfPointByReturn[2] == 1500443);
        Assert.That(header.LegacyNumberOfPointByReturn[3] == 121130);
        Assert.That(header.LegacyNumberOfPointByReturn[4] == 7778);
        Assert.That(header.XScaleFactor == 0.001);
        Assert.That(header.YScaleFactor == 0.001);
        Assert.That(header.ZScaleFactor == 0.001);
        Assert.That(header.XOffset == 376331.560);
        Assert.That(header.YOffset == 3757833.285);
        Assert.That(header.ZOffset == 27.238);
        Assert.That(header.MaxX == 376899.027);
        Assert.That(header.MinX == 375764.094);
        Assert.That(header.MaxY == 3758462.189);
        Assert.That(header.MinY == 3757204.382);
        Assert.That(header.MaxZ == 138.186);
        Assert.That(header.MinZ == -83.709);

        Assert.That(header.WaveformDataOffset == 0);
        Assert.That(header.EvlrOffset == 2029271627);
        Assert.That(header.EvlrCount == 1);
        Assert.That(header.PointCount == 364384576);

        Assert.That(header.PointCountByReturn[0] == 347247515);
        Assert.That(header.PointCountByReturn[1] == 15507300);
        Assert.That(header.PointCountByReturn[2] == 1500443);
        Assert.That(header.PointCountByReturn[3] == 121130);
        Assert.That(header.PointCountByReturn[4] == 7778);
        Assert.That(header.PointCountByReturn[5] == 390);
        Assert.That(header.PointCountByReturn[6] == 19);
        Assert.That(header.PointCountByReturn[7] == 1);
        Assert.That(header.PointCountByReturn[8] == 0);
        Assert.That(header.PointCountByReturn[9] == 0);
        Assert.That(header.PointCountByReturn[10] == 0);
        Assert.That(header.PointCountByReturn[11] == 0);
        Assert.That(header.PointCountByReturn[12] == 0);
        Assert.That(header.PointCountByReturn[13] == 0);
        Assert.That(header.PointCountByReturn[14] == 0);
            
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