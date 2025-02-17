﻿using System.Text;

namespace Copc.Las;
public static class VlrReader
{
    public static Vlr Read(BinaryReader reader, bool isExtended = false)
    {
        var vlrInfo = new Vlr();
        vlrInfo.Reserved = reader.ReadUInt16(); // reserved
        vlrInfo.UserId = Encoding.ASCII.GetString(reader.ReadBytes(16)).Trim('\0');
        vlrInfo.RecordId = reader.ReadUInt16();
        vlrInfo.RecordLength = isExtended ? (long)reader.ReadUInt64() : reader.ReadUInt16();
        vlrInfo.Description = Encoding.ASCII.GetString(reader.ReadBytes(32)).Trim('\0');
        vlrInfo.IsExtended = isExtended;
        return vlrInfo;
    }
}
