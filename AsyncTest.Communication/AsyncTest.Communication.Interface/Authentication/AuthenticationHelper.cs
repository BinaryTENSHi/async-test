using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Interface.Authentication
{
    public static class AuthenticationHelper
    {
        public static async Task<string> ComputeContentHashAsync(HttpContent httpContent)
        {
            if (httpContent == null)
                return null;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] content = await httpContent.ReadAsByteArrayAsync().ConfigureAwait(false);
                return content.Length != 0
                    ? Convert.ToBase64String(sha256.ComputeHash(content))
                    : null;
            }
        }

        public static string CalculateHMACSHA256(string key, string data)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] hash = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static byte[] SecureBytes(int length)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Create().GetNonZeroBytes(bytes);
            return bytes;
        }

        public static string CreateAuthenticationHeader(string appId, string signature, string nonce, long timestamp)
            => string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}:{3}", appId, signature, nonce, timestamp);

        public static string CreateSignatureData(string appId, HttpRequestMessage request, long timestamp, string nonce, string contentHash)
        {
            string uri = request.RequestUri.AbsolutePath.ToUpperInvariant();
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}", appId, request.Method, uri, timestamp, nonce, contentHash);
        }
    }
}