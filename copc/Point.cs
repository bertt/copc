namespace copc;
public class Point
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }
    public ushort Intensity { get; set; }
    public byte ReturnNumber { get; set; }
    public byte NumberOfReturns { get; set; }
    public byte ClassificationFlags { get; set; }
    public byte ScannerChannel { get; set; }
    public bool ScanDirectionFlag { get; set; }
    public bool EdgeOfFlightLine { get; set; }
    public byte Classification { get; set; }
    public byte UserData { get; set; }
    public short ScanAngle { get; set; }
    public ushort PointSourceId { get; set; }
    public double GpsTime { get; set; }
}
