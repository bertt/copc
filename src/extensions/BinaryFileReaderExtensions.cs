using Copc.Io;
using Copc.Las;
using System.Reflection.Metadata;
using System.Text;

namespace Copc;
public static class BinaryFileReaderExtensions
{
    public static async Task<Copc> Read(this BinaryFileReader binaryFileReader)
    {
        var headerBytes = await binaryFileReader.Read(0, 589);
        var reader = new BinaryReader(new MemoryStream(headerBytes));
        var copc = reader.ReadCopc();
        var header = copc.Header;
        var vlrCount = (int)header.VlrCount;
        var vlrs = await binaryFileReader.GetVlrs(vlrCount);
        copc.Vlrs.AddRange(vlrs);
        return copc;
    }

    private static async Task<List<Vlr>> GetVlrs(this BinaryFileReader binaryFileReader, int vlrCount)
    {
        var vlrs = new List<Vlr>();
        var start = 589;
        for (var i = 1; i < vlrCount; i++)
        {
            var end = start + 54;
            var vlr = await GetVlr(binaryFileReader, start, end);
            vlrs.Add(vlr);

            start = end;
            end = start + vlr.RecordLength;
            var recordBytes = await binaryFileReader.Read(start, end-1);
            var recordReader = new BinaryReader(new MemoryStream(recordBytes));
            vlr.ContentOffset = start;
            if (vlr.RecordId == 22204 && vlr.UserId == "laszip encoded")
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
                vlr.Data = lazVlr;
            }
            else if (vlr.RecordId == 2112 && vlr.UserId == "LASF_Projection")
            {
                var wkt = Encoding.UTF8.GetString(recordBytes).TrimEnd('\0');
                vlr.Data = wkt;
            }
            // todo add others like what?
            start = end;
        }

        return vlrs;
    }

    private static async Task<Vlr> GetVlr(this BinaryFileReader processor, int start, int end)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrReader.Read(reader);
        return vlr;
    }

}
