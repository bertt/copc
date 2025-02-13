using Copc;
using Copc.Las;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace copc;

public class Tests
{
    [Test]
    public async Task ReadCopcFromFile()
    {
        var file = "./testfixtures/ellipsoid.copc.laz";
        var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, file);

        var copc = await CopcReader.Read(file);
        var header = copc.Header;
        Assert.That(header.FileSignature == "LASF");
        Assert.That(copc.Vlrs.Count == 4);
        Assert.That(copc.Vlrs[1].ContentOffset == 643);
        var lazVlr = (LazVlr)copc.Vlrs[1].Data;
        Assert.That(lazVlr.ChunkSize == 4294967295);
        Assert.That(lazVlr.Compressor == 3);
        Assert.That(lazVlr.Coder == 0);
        Assert.That(lazVlr.EvlrsCount == -1);
        Assert.That(lazVlr.EvlrsOffset == -1);
        Assert.That(lazVlr.NumberOfItems == 2);
        Assert.That(lazVlr.Options == 0);
        Assert.That(lazVlr.VersionMajor == 3);
        Assert.That(lazVlr.VersionMinor == 4);
        Assert.That(lazVlr.VersionRevision == 3);
        Assert.That(copc.Vlrs[2].ContentOffset == 743);
        Assert.That(((string)copc.Vlrs[2].Data) == "PROJCS[\"WGS 84 / Pseudo-Mercator\",GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.0174532925199433,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]],PROJECTION[\"Mercator_1SP\"],PARAMETER[\"central_meridian\",0],PARAMETER[\"scale_factor\",1],PARAMETER[\"false_easting\",0],PARAMETER[\"false_northing\",0],UNIT[\"metre\",1,AUTHORITY[\"EPSG\",\"9001\"]],AXIS[\"Easting\",EAST],AXIS[\"Northing\",NORTH],EXTENSION[\"PROJ4\",\"+proj=merc +a=6378137 +b=6378137 +lat_ts=0 +lon_0=0 +x_0=0 +y_0=0 +k=1 +units=m +nadgrids=@null +wktext +no_defs\"],AUTHORITY[\"EPSG\",\"3857\"]]");
        
        Assert.That(copc.Vlrs[3].ContentOffset == 630520 + 60);
        Assert.That(copc.Vlrs[3].RecordLength == 160);
        Assert.That(copc.Vlrs[3].RecordId == 1000);
        Assert.That(copc.Vlrs[3].Description == "EPT Hierarchy");
        Assert.That(copc.Vlrs[3].UserId == "copc");
        Assert.That(copc.Vlrs[3].Reserved == 0);

        Assert.That(copc.HierarchyPages.Count == 5);
        var firstHierarchyPage = copc.HierarchyPages[0];
        Assert.That(firstHierarchyPage.Depth == 0);
        Assert.That(firstHierarchyPage.X == 0);
        Assert.That(firstHierarchyPage.Y == 0);
        Assert.That(firstHierarchyPage.Z == 0);
        Assert.That(firstHierarchyPage.PointCount == 66272);
        Assert.That(firstHierarchyPage.PointDataOffset == 1432);
        Assert.That(firstHierarchyPage.PointDataLength == 358488);

        // request root hierachy page
        var rootHierarchyPage = copc.HierarchyPages.First();

    }

    [Test]
    public async Task ReadCOPCFromUrl()
    {
        // 589 bytes
        // 2.3GB sofi stadium
        var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";
        var httpClient = new HttpClient();
        var copc = await CopcReader.Read(httpClient, url);

        var header = copc.Header;
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
        Assert.That(header.VlrCount == 3);
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

        var vlrInfo = copc.Vlrs.First();
        Assert.That(vlrInfo.Reserved == 0);
        Assert.That(vlrInfo.UserId == "copc");
        Assert.That(vlrInfo.RecordId == 1);
        Assert.That(vlrInfo.RecordLength == 160);
        Assert.That(vlrInfo.Description == "COPC info VLR");

        var copcInfo = (CopcInfo)vlrInfo.Data;
        Assert.That(copcInfo.CenterX == 376392.99749999976);
        Assert.That(copcInfo.CenterY == 3757833.2855);
        Assert.That(copcInfo.CenterZ == 545.194499999782);
        Assert.That(copcInfo.HalfSize == 628.90349999978207);
        Assert.That(copcInfo.Spacing == 9.8266171874965949);
        Assert.That(copcInfo.RootHierarchyOffset == 2029609895);
        Assert.That(copcInfo.RootHierarchySize == 86720);
        Assert.That(copcInfo.GpsTimeMinimum == 285222.22806577);
        Assert.That(copcInfo.GpsTimeMaximum == 285222.22806577);
        Assert.That(copc.Vlrs.Count == 4);
        Assert.That(copc.Vlrs[0].IsExtended == false);
        Assert.That(copc.Vlrs[3].Description == "EPT Hierarchy");
        Assert.That(copc.Vlrs[3].RecordLength == 424928);
        Assert.That(copc.Vlrs[3].ContentOffset == 2029271687);
        Assert.That(copc.Vlrs[3].IsExtended = true);

        Assert.That(copc.HierarchyPages.Count == 13279);

        // select all hierarchy pages with key 4-3-3-1
        var hierarchyPages = copc.HierarchyPages.Where(h => h.Key == "4-3-3-1").ToList();
        Assert.That(hierarchyPages.Count == 2); 
        Assert.That(hierarchyPages[0].PointCount == 19164);
        Assert.That(hierarchyPages[1].PointCount == -1); // parent hierarchy page

        var parentHierarchyPages = copc.HierarchyPages.Where(h => h.PointCount == -1).ToList();
        Assert.That(parentHierarchyPages.Count == 111); // 111 cases with parent - child hierarchy pages
    }
}