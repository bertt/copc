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
    }
}