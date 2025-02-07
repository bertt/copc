using System.Text;

namespace copc.las
{
    public static class LasHeaderReader
    {
        public static LasHeader Read(BinaryReader reader)
        {
            var header = new LasHeader();
            header.FileSignature = Encoding.Default.GetString(reader.ReadBytes(4));
            header.FileSourceId = reader.ReadUInt16();
            header.GlobalEncoding = reader.ReadUInt16();
            header.ProjectId = new Guid(reader.ReadInt32(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadBytes(8));
            header.LasMajorVersion = reader.ReadByte();
            header.LasMinorVersion = reader.ReadByte();
            header.SystemIdentifier = Encoding.Default.GetString(reader.ReadBytes(32)).TrimEnd('\0');
            header.GeneratingSoftware = Encoding.Default.GetString(reader.ReadBytes(32)).TrimEnd('\0');
            header.FileCreationDayOfYear = reader.ReadUInt16();
            header.FileCreationYear = reader.ReadUInt16();
            header.HeaderSize = reader.ReadUInt16();
            header.PointDataOffset = reader.ReadUInt32();
            header.VlrCount = reader.ReadUInt32();
            header.PointDataRecordFormat = reader.ReadByte() & 0b1111;
            header.PointDataRecordLength = reader.ReadUInt16();
            header.LegacyNumberOfPointRecords = reader.ReadUInt32();

            header.LegacyNumberOfPointByReturn = Enumerable.Range(0, 5)
                .Select(_ => (ulong)reader.ReadUInt32())
                .ToArray();

            header.XScaleFactor = reader.ReadDouble();
            header.YScaleFactor = reader.ReadDouble();
            header.ZScaleFactor = reader.ReadDouble();
            header.XOffset = reader.ReadDouble();
            header.YOffset = reader.ReadDouble();
            header.ZOffset = reader.ReadDouble();
            header.MaxX = reader.ReadDouble();
            header.MinX = reader.ReadDouble();
            header.MaxY = reader.ReadDouble();
            header.MinY = reader.ReadDouble();
            header.MaxZ = reader.ReadDouble();
            header.MinZ = reader.ReadDouble();
            header.WaveformDataOffset = reader.ReadInt64();
            header.EvlrOffset = reader.ReadInt64();
            header.EvlrCount = reader.ReadUInt32();
            header.PointCount = reader.ReadInt64();

            header.PointCountByReturn = Enumerable.Range(0, 15)
                .Select(_ => reader.ReadInt64())
                .ToArray();

            return header;
        }
    }
}
