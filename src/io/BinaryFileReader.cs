
namespace Copc.Io;

public abstract class BinaryFileReader
{
    protected readonly string source;
    public BinaryFileReader(string source) => this.source = source;
    public abstract Task<byte[]> Read(long start, long end);
}
