namespace copc.copc;

public class CopcInfo
{
    public double CenterX { get; set; }
    public double CenterY { get; set; }
    public double CenterZ { get; set; }

    public double HalfSize { get; set; }

    public double Spacing { get; set; }

    public ulong RootHierarchyOffset { get; set; }

    public ulong RootHierarchySize { get; set; }

    public double GpsTimeMinimum { get; set; }

    public double GpsTimeMaximum { get; set; }
}