using System.Collections.Generic;
using System.IO;

namespace Builder.Plugins
{
    class AddBytes
    {
        public static void Add(string path, long bytes)
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(File.ReadAllBytes(path));
            byte[] numArray = new byte[bytes];
            byteList.AddRange(numArray);
            FileInfo fileInfo = new FileInfo(path);
            File.WriteAllBytes(string.Format(path, fileInfo.DirectoryName, fileInfo.Name), byteList.ToArray());
        }
    }
}
