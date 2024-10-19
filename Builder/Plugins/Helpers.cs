using System;
using System.Linq;

namespace Builder.Plugins
{
    public static class Helpers
    {
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

        public static string DecodeBase64String(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}
