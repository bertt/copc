namespace copc.io;

public class BinaryFileProcessor
{
    private readonly BinaryFileReader reader;

    public BinaryFileProcessor(BinaryFileReader reader) => this.reader = reader;

    public async Task<byte[]> Read(long start, long end)
    {
        return await reader.Read(start, end);
    }
}
