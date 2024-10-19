using System.Collections.Generic;

namespace Server.RenamingObfuscation.Classes
{
    internal static class DecryptionHelper
    {
        public static string Module(string t)
        {
            string k = "[KEY]";
            byte[] r = new byte[t.Length];
            int i = 0;
            for (int j = 0; j < t.Length; j++)
            {
                r[j] = (byte)(t[j] ^ k[i++]);
                i %= k.Length;
            }
            return System.Text.Encoding.Default.GetString(r);
        }

    }
}
