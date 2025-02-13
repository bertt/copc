namespace copc.copc
{
    public class HierarchyPage
    {
        public int Depth { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public ulong PointDataOffset { get; set; }
        public int PointDataLength { get; set; }
        public int PointCount { get; set; }
        public string Key => $"{Depth}-{X}-{Y}-{Z}";

    }
}
