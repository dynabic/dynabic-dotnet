#pragma warning disable 1591

using System;
using System.Security.Cryptography;
using System.Text;

namespace DynabicPlatform.Classes
{
    public class Crypto
    {
        public const string SIGNATURE_PARAM = "signature";
        public const string CLIENT_KEY_PARAM = "clientkey";

        public static string SignString(string content, string signingKey)
        {
            var encoding = new UTF8Encoding();
            var signingKeyBytes = encoding.GetBytes(signingKey);
            var contentBytes = encoding.GetBytes(content.ToLower());

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(signingKeyBytes);
            byte[] hash = algorithm.ComputeHash(contentBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            return Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }

        public static string SignUrl(string url, string clientKey, string signingKey)
        {
            char separator;
            if (url.IndexOf('?') == -1)
            {
                separator = '?';
            }
            else
            {
                separator = '&';
            }
            url += string.Format("{0}{1}={2}", separator, CLIENT_KEY_PARAM, clientKey);
            return url + string.Format("&{0}={1}", SIGNATURE_PARAM, SignString(url, signingKey));
        }

        public virtual String HmacHash(String key, String message)
        {
            var hmac = new HMACSHA1(Sha1Bytes(key));
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public virtual byte[] Sha1Bytes(String s)
        {
            byte[] data = Encoding.UTF8.GetBytes(s);
            return new SHA1CryptoServiceProvider().ComputeHash(data);
        }
    }
}
