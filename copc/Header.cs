namespace copc
{
    public class Header
    {
        public string FileSignature { get; set; }
        public string CopcSignature { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PointDataRecordFormat { get; set; }

        public int PointDataRecordLength { get; set; }
        public ushort FileSourceId { get; internal set; }
        public ushort GlobalEncoding { get; internal set; }
        public Guid ProjectId { get; internal set; }
        public byte LasMajorVersion { get; internal set; }
        public byte LasMinorVersion { get; internal set; }
    }
}