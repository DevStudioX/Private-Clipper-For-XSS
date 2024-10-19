using Stub.TelegramAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stub.Help.Modules
{
    class GetStringDownload
    {
        public static async Task Run(string url)  
        {
            try
            {
                SSL.GetSSL();
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch { }
        }
    }
}
