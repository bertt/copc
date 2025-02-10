namespace Copc.Io;

public class LocalBinaryFileReader : BinaryFileReader
{
    public LocalBinaryFileReader(string source) : base(source)
    {
    }
    public override async Task<byte[]> Read(long start, long end)
    {
        long length = end - start + 1;
        byte[] buffer = new byte[length];

        using (var fs = new FileStream(source, FileMode.Open, FileAccess.Read))
        {
            fs.Seek(start, SeekOrigin.Begin);
            await fs.ReadAsync(buffer, 0, buffer.Length);
        }

        return buffer;
    }

}
