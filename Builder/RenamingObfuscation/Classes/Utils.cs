using System;
using System.Text;

namespace Server.RenamingObfuscation.Classes
{
    public static class Utils
    {
        public static string GenerateRandomString()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= random.Next(16, 28); i++)
            {
                var randomCharacterPosition = random.Next(0, alphabet.Length);
                sb.Append(alphabet[randomCharacterPosition]);
            }
            return  "MODULE_" + sb.ToString();
        }

        private static readonly Random random = new Random();
        static string alphabet = @"_qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろゎわゐゑをんゔゕゖ゙゚゛゜ゝゞゟ゠ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロヮワヰヱヲンヴヵヶヷヸヹヺ・ーヽヾヿ㍐㍿々〒〜〃※〆";
    }
}
