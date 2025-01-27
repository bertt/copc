﻿namespace copc
{
    public class Header
    {
        public string FileSignature { get; set; }
        public ushort FileSourceId { get; internal set; }
        public ushort GlobalEncoding { get; internal set; }
        public Guid ProjectId { get; internal set; }
        public byte LasMajorVersion { get; internal set; }
        public byte LasMinorVersion { get; internal set; }

        public string SystemIdentifier { get; internal set; }
        public string GeneratingSoftware { get; internal set; }
        public ushort FileCreationDayOfYear { get; internal set; }
        public ushort FileCreationYear { get; internal set; }
        public ushort HeaderSize { get; internal set; }
        public ulong PointDataOffset { get; internal set; }
        public ulong NumberOfVariableLengthRecords { get; internal set; }

        public int PointDataRecordFormat { get; set; }

        public int PointDataRecordLength { get; set; }

        public ulong LegacyNumberOfPointRecords { get; internal set; }

        public ulong[] LegacyNumberOfPointByReturn { get; internal set; }

        public double XScaleFactor { get; internal set; }
        public double YScaleFactor { get; internal set; }

        public double ZScaleFactor { get; internal set; }

        public double XOffset { get; internal set; }
        public double YOffset { get; internal set; }
        public double ZOffset { get; internal set; }

        public double MaxX { get; internal set; }

        public double MinX { get; internal set; }
        public double MaxY { get; internal set; }
        public double MinY { get; internal set; }
        public double MaxZ { get; internal set; }
        public double MinZ { get; internal set; }

        public long WaveformDataOffset { get; set; }

        public long EvlrOffset { get; set; }

        public ulong EvlrCount { get; set; }

        public long PointCount { get; set; }

        public long[] PointCountByReturn { get; set; }

        public string CopcSignature { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

    }
}