using Stub.Help.Native;
using System;
using System.Runtime.InteropServices;

namespace Stub.Help.Modules
{
    public static class USBWatcher
    {
        public static void WatchUSB()
        {
            NativeMethods.DEV_BROADCAST_VOLUME volumeDevice = new NativeMethods.DEV_BROADCAST_VOLUME();
            volumeDevice.dbcv_size = Marshal.SizeOf(volumeDevice);
            volumeDevice.dbcv_devicetype = 0x00000002; // DBT_DEVTYP_VOLUME

            IntPtr notificationHandle = NativeMethods.RegisterDeviceNotification(IntPtr.Zero, ref volumeDevice, 0);

            try
            {
                while (true)
                {
                    System.Windows.Forms.Application.DoEvents(); // Обработка сообщений
                }
            }
            finally
            {
                NativeMethods.UnregisterDeviceNotification(notificationHandle);
            }
        }

        public static string GetDriveLetterFromMask(int unitmask)
        {
            for (int i = 0; i < 26; i++)
            {
                if ((unitmask & (1 << i)) != 0)
                {
                    char driveLetter = (char)('A' + i);
                    return driveLetter + ":\\";
                }
            }
            return null;
        }
    }
}
