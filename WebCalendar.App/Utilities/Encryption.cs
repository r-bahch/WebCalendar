using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebCalendar.App.Utilities
{
    public class Encryption
    {
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}