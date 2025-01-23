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
            header.FileSourceId = BitConverter.ToUInt16(headerBytes.Skip(4).Take(2).ToArray());
            header.GlobalEncoding = BitConverter.ToUInt16(headerBytes.Skip(6).Take(2).ToArray());
            var projectId1 = BitConverter.ToInt32(headerBytes.Skip(8).Take(4).ToArray());
            var projectId2 = (short)BitConverter.ToUInt16(headerBytes.Skip(12).Take(2).ToArray());
            var projectId3 = (short)BitConverter.ToUInt16(headerBytes.Skip(14).Take(2).ToArray());
            var projectId4 = headerBytes.Skip(16).Take(8).ToArray();
            header.ProjectId = new Guid(projectId1, projectId2, projectId3, projectId4);
            header.LasMajorVersion = headerBytes.Skip(24).FirstOrDefault();
            header.LasMinorVersion = headerBytes.Skip(25).FirstOrDefault();

            // Todo read many more fields

            header.CopcSignature = Encoding.UTF8.GetString(headerBytes.Skip(377).Take(4).ToArray());
            header.MajorVersion = headerBytes.Skip(393).FirstOrDefault();
            header.MinorVersion = headerBytes.Skip(394).FirstOrDefault();
            header.PointDataRecordFormat = headerBytes.Skip(104).FirstOrDefault() & 0b1111;
            header.PointDataRecordLength = BitConverter.ToUInt16(headerBytes.Skip(105).Take(2).ToArray());
            return header;
        }
    }
}
