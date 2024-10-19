using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Stub.TelegramAPI
{
    internal class SSL
    {
        public static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error) => error == SslPolicyErrors.None;

        public static void GetSSL()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => ValidateRemoteCertificate(sender, cert, chain, errors);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        }
    }
}
