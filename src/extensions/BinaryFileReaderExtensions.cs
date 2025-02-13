using copc.copc;
using Copc.Io;
using Copc.Las;
using System.Diagnostics;
using System.Text;

namespace Copc;
public static class BinaryFileReaderExtensions
{
    public static async Task<Copc> Read(this BinaryFileReader binaryFileReader)
    {
        var headerBytes = await binaryFileReader.Read(0, 375);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var header = LasHeaderReader.Read(reader);

        var copc = new Copc(header);

        var copcVlr = await binaryFileReader.GetVlrs(375, 1);
        copc.Vlrs.Add(copcVlr.FirstOrDefault());

        var vlrCount = (int)header.VlrCount;
        var vlrs = await binaryFileReader.GetVlrs(589, vlrCount - 1);
        copc.Vlrs.AddRange(vlrs);
        var evlrs = await binaryFileReader.GetVlrs(header.EvlrOffset, (int)header.EvlrCount, true);
        copc.Vlrs.AddRange(evlrs);

        var hierarchyEvlr = copc.Vlrs.FirstOrDefault(v => v.RecordId == 1000 && v.UserId == "copc");
        if (hierarchyEvlr != null)
        {
            var hierarchyItemLength = 32;
            var bytes = (byte[])hierarchyEvlr.Data;
            var binaryReader = new BinaryReader(new MemoryStream(bytes));

            for (var i = 0; i < bytes.Length; i += hierarchyItemLength)
            {
                var d = binaryReader.ReadInt32();
                var x = binaryReader.ReadInt32();
                var y = binaryReader.ReadInt32();
                var z = binaryReader.ReadInt32();
                var offset = binaryReader.ReadUInt64();
                var length = binaryReader.ReadInt32();
                var pointCount = binaryReader.ReadInt32();

                var hierarchyPage = new HierarchyPage
                {
                    Depth = d,
                    X = x,
                    Y = y,
                    Z = z,
                    PointDataOffset = offset,
                    PointDataLength = length,
                    PointCount = pointCount
                };

                var key = $"{d}-{x}-{y}-{z}";
                Debug.WriteLine("KEY: "+ key);
                copc.HierarchyPages.Add(hierarchyPage);
            }
        }
        return copc;
    }


    private static async Task<List<Vlr>> GetVlrs(this BinaryFileReader binaryFileReader, long offset, int vlrCount, bool isExtendes = false)
    {
        var vlrs = new List<Vlr>();
        for (var i = 1; i <= vlrCount; i++)
        {
            var end = isExtendes? offset + 60: offset + 54;
            var vlr = await GetVlr(binaryFileReader, offset, end, isExtendes);
            vlrs.Add(vlr);

            offset = end;
            end = offset + vlr.RecordLength;
            var recordBytes = await binaryFileReader.Read(offset, end - 1);
            var binaryReader = new BinaryReader(new MemoryStream(recordBytes));
            vlr.ContentOffset = offset;

            switch (vlr.RecordId)
            {
                case 22204 when vlr.UserId == "laszip encoded":
                    {
                        var lazVlr = ReadLazVlr(binaryReader);
                        vlr.Data = lazVlr;
                        break;
                    }
                case 2112 when vlr.UserId == "LASF_Projection":
                    {
                        var wkt = Encoding.UTF8.GetString(recordBytes);
                        vlr.Data = wkt;
                        break;
                    }
                case 1000 when vlr.UserId == "copc":
                    {
                        // var nodeCount = recordBytes.ReadUInt32();
                        vlr.Data = recordBytes;
                        break;
                    }
                case 1 when vlr.UserId == "copc":
                    {
                        var copcInfoReader = CopcInfoReader.Read(binaryReader);
                        vlr.Data = copcInfoReader;
                        break;
                    }
            }

            // todo add others like what?
            offset = end;
        }

        return vlrs;
    }

    private static LazVlr ReadLazVlr(BinaryReader recordReader)
    {
        var lazVlr = new LazVlr();
        lazVlr.Compressor = recordReader.ReadUInt16();
        lazVlr.Coder = recordReader.ReadUInt16();
        lazVlr.VersionMajor = recordReader.ReadByte();
        lazVlr.VersionMinor = recordReader.ReadByte();
        lazVlr.VersionRevision = recordReader.ReadUInt16();
        lazVlr.Options = recordReader.ReadUInt32();
        lazVlr.ChunkSize = recordReader.ReadUInt32();
        lazVlr.EvlrsCount = recordReader.ReadInt64();
        lazVlr.EvlrsOffset = recordReader.ReadInt64();
        lazVlr.NumberOfItems = recordReader.ReadInt16();

        for (var j = 0; j < lazVlr.NumberOfItems; j++)
        {
            var item = new LazVlrItem();
            item.Type = recordReader.ReadUInt16();
            item.Size = recordReader.ReadUInt16();
            item.Version = recordReader.ReadUInt16();
            lazVlr.Items.Add(item);
        }

        return lazVlr;
    }

    private static async Task<Vlr> GetVlr(this BinaryFileReader processor, long start, long end, bool isExtended = false)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrReader.Read(reader, isExtended);
        return vlr;
    }

}
