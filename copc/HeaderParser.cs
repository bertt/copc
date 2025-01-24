using System.Text;

namespace copc
{
    public static class HeaderParser
    {
        public static Header ParseHeader(byte[] headerBytes)
        {
            var header = new Header();
            var reader = new BinaryReader(new MemoryStream(headerBytes));
            header.FileSignature = Encoding.Default.GetString(reader.ReadBytes(4));
            header.FileSourceId = reader.ReadUInt16();
            header.GlobalEncoding = reader.ReadUInt16();
            var projectId1 = reader.ReadInt32();
            var projectId2 = reader.ReadInt16();
            var projectId3 = reader.ReadInt16();
            var projectId4 = reader.ReadBytes(8);
            header.ProjectId = new Guid(projectId1, projectId2, projectId3, projectId4);
            header.LasMajorVersion = reader.ReadByte();
            header.LasMinorVersion = reader.ReadByte();
            header.SystemIdentifier = Encoding.Default.GetString(reader.ReadBytes(32)).Replace("\0", string.Empty);
            header.GeneratingSoftware = Encoding.Default.GetString(reader.ReadBytes(32)).Replace("\0", string.Empty);
            header.FileCreationDayOfYear = reader.ReadUInt16();
            header.FileCreationYear = reader.ReadUInt16();
            header.HeaderSize = reader.ReadUInt16();
            header.PointDataOffset = reader.ReadUInt32();
            header.NumberOfVariableLengthRecords = reader.ReadUInt32();
            header.PointDataRecordFormat = reader.ReadByte() & 0b1111;
            header.PointDataRecordLength = reader.ReadUInt16();
            header.LegacyNumberOfPointRecords = reader.ReadUInt32();
            header.LegacyNumberOfPointByReturn = new ulong[5];
            for (var i = 0; i < 5; i++)
            {
                header.LegacyNumberOfPointByReturn[i] = reader.ReadUInt32();
            }
            header.XScaleFactor = reader.ReadDouble();
            header.YScaleFactor = reader.ReadDouble();
            header.ZScaleFactor = reader.ReadDouble();

            header.XOffset = reader.ReadDouble();
            header.YOffset = reader.ReadDouble();
            header.ZOffset = reader.ReadDouble();

            // todo read more properties
            return header;
        }
    }
}
