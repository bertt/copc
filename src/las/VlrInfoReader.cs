using System.Text;

namespace Copc.Las;
public static class VlrInfoReader
{
    public static VlrInfo Read(BinaryReader reader)
    {
        var vlrInfo = new VlrInfo();
        vlrInfo.Reserved = reader.ReadUInt16(); // reserved
        vlrInfo.UserId = Encoding.ASCII.GetString(reader.ReadBytes(16)).TrimEnd('\0');
        vlrInfo.RecordId = reader.ReadUInt16();
        vlrInfo.RecordLength = reader.ReadUInt16();
        vlrInfo.Description = Encoding.ASCII.GetString(reader.ReadBytes(32)).TrimEnd('\0');
        return vlrInfo;
    }
}
