using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Util
{
    public static class Utils
    {
        internal static string KeyMaker(string type, int key)
        {
            return type + "::" + key;
        }
        internal static string KeyMaker(string type, string key)
        {
            return type + "::" + key;
        }
        internal static string KeyMaker(string type, int key, string suffix)
        {
            return type + "::" + key + "_" + suffix;
        }

        public static string CalculateHash(string pass)
        {


            using (SHA256 hash = SHA256Managed.Create())
            {

                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(pass))
                  .Select(item => item.ToString("x2")));

            }
        }
        public static long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static string ToBase64(string Path)
        {

            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
