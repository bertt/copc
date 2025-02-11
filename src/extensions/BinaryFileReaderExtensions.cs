using Copc.Io;
using Copc.Las;
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
        var vlrs = await binaryFileReader.GetVlrs(589, vlrCount-1);
        copc.Vlrs.AddRange(vlrs);
        var evlrs = await binaryFileReader.GetVlrs(header.EvlrOffset, (int)header.EvlrCount);
        copc.Evlrs = evlrs;
        return copc;
    }


    private static async Task<List<Vlr>> GetVlrs(this BinaryFileReader binaryFileReader, long offset, int vlrCount)
    {
        var vlrs = new List<Vlr>();
        for (var i = 1; i <= vlrCount; i++)
        {
            var end = offset + 54;
            var vlr = await GetVlr(binaryFileReader, offset, end);
            vlrs.Add(vlr);

            offset = end;
            end = offset + vlr.RecordLength;
            var recordBytes = await binaryFileReader.Read(offset, end-1);
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
                        // todo parse the bytes? something with voxelKey, entry
                        vlr.Data = recordBytes;
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

    private static async Task<Vlr> GetVlr(this BinaryFileReader processor, long start, long end)
    {
        var headerBytes1 = await processor.Read(start, end);
        var reader = new BinaryReader(new MemoryStream(headerBytes1));
        var vlr = VlrReader.Read(reader);
        return vlr;
    }

}
