namespace copc;
public static class CopcInfoReader
{
    public static CopcInfo Read(BinaryReader reader)
    {
        var copcInfo = new CopcInfo();
        copcInfo.CenterX = reader.ReadDouble();
        copcInfo.CenterY = reader.ReadDouble();
        copcInfo.CenterZ = reader.ReadDouble();
        copcInfo.HalfSize = reader.ReadDouble();
        copcInfo.Spacing = reader.ReadDouble();
        copcInfo.RootHierarchyOffset = reader.ReadUInt64();
        copcInfo.RootHierarchySize = reader.ReadUInt64();
        copcInfo.GpsTimeMinimum = reader.ReadDouble();
        copcInfo.GpsTimeMaximum = reader.ReadDouble();
        return copcInfo;
    }
}
