using System.Text;

namespace copc
{
    public static class HeaderParser
    {
        public static Header ParseHeader(byte[] headerBytes)
        {
            // specs see https://copc.io/
            var header = new Header();
            header.FileSignature = Encoding.UTF8.GetString(headerBytes.Take(4).ToArray());
            header.CopcSignature = Encoding.UTF8.GetString(headerBytes.Skip(377).Take(4).ToArray());
            header.MajorVersion = headerBytes.Skip(393).FirstOrDefault();
            header.MinorVersion = headerBytes.Skip(394).FirstOrDefault();
            header.PointDataRecordFormat = headerBytes.Skip(104).FirstOrDefault() & 0b1111;
            header.PointDataRecordLength = BitConverter.ToUInt16(headerBytes.Skip(105).Take(2).ToArray());
            return header;
        }
    }
}
